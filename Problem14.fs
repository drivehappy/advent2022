module Problem14

open System.IO

let gridX = 500
let gridY = 200
let f = File.ReadAllLines("../../../input14.txt")


type Cell =
    | Rock
    | Air
    | Sand

type Pos = 
    { x: int
      y: int 
    }

let grid: Cell array array = Array.zeroCreate gridY
for y in 0..gridY-1 do
    grid[y] <- Array.zeroCreate gridX
    for x in 0..gridX-1 do
        grid[y][x] <- Air


// Input seems to be roughly between 440 - 520
let convX (i : int) : int = i - 250
let convY (i : int) : int = i

let mutable maxY = 0

let mutable rockSpan =
    f
    |> Array.map (fun s ->
        let spanCoords =
            s.Split(" -> ")
            |> Array.map (fun s2 ->
                let g = s2.Split(",") |> Array.map int
                if g[1] > maxY then maxY <- g[1]
                { x = convX g[0]; y = convY g[1] }
            )
        spanCoords
    )

//printfn "Spans: %A" rockSpan
    
let floorY = maxY + 2

let populateGrid (p1: Pos) (p2: Pos) =
    if p1.x = p2.x then
        // Vert
        if p2.y > p1.y then
            for y in p1.y..p2.y do
                grid[y][p1.x] <- Rock
        else
            for y in p2.y..p1.y do
                grid[y][p1.x] <- Rock
    else
        // Horz
        if p2.x > p1.x then
            for x in p1.x..p2.x do
                grid[p1.y][x] <- Rock
        else
            for x in p2.x..p1.x do
                grid[p1.y][x] <- Rock

// Populate grid with input rockSpan
rockSpan
|> Array.iter (fun span ->
    for s in 0..span.Length-2 do
        let (p1, p2) = (span[s], span[s+1])
        populateGrid p1 p2
)

// Add floor
populateGrid
    { x = 0; y = maxY + 2 }
    { x = gridX-1; y = maxY + 2 }

//
let printGrid () =
    printfn "Grid:"

    grid
    |> Array.iter (fun y ->
        y
        |> Array.iter (fun c ->
            let s = 
                match c with
                | Rock -> "#"
                | Air -> "."
                | Sand -> "o"

            printf "%s" s
        )

        printfn ""
    )

//
//printGrid()

let posClear (s: Pos) : bool =
    match grid[s.y][s.x] with
    | Air -> true
    | Sand
    | Rock -> false

let checkDown (s: Pos) : bool =
    posClear ({ s with y = s.y + 1; x = s.x})

let checkDownLeft (s: Pos) : bool =
    posClear ({ s with y = s.y + 1; x = s.x - 1})

let checkDownRight (s: Pos) : bool =
    posClear ({ s with y = s.y + 1; x = s.x + 1})


// Returns new sand position and whether it has stopped
let singleStep (s: Pos) : (Pos * bool) =
    if checkDown s then
        ({ s with y = s.y + 1 }, false)
    elif checkDownLeft s then
        ({ s with y = s.y + 1; x = s.x - 1 }, false)
    elif checkDownRight s then
        ({ s with y = s.y + 1; x = s.x + 1 }, false)
    else
        (s, true)


let mutable c = 0
let mutable loop = true
while loop do
    c <- c + 1

    // Run a new grain of sand
    let mutable inLoop = true
    let initS = { x = convX 500; y = convY 0 }
    let mutable s = initS
    while inLoop do
        // Check solved (grain has fallen off)
        if s.y + 1 >= gridY then
            inLoop <- false
            loop <- false
        elif not (posClear initS) then
            // Soln2 solved
            inLoop <- false
            loop <- false
        else
            let (newPos, stopped) = singleStep s
            if stopped then
                s <- newPos
                grid[s.y][s.x] <- Sand
                inLoop <- false
            else
                s <- newPos

    //if c < 5 then
        //printGrid()
        //loop <- false

printfn "Soln1: %i" (c-1)