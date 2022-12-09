namespace prob9

(*
open System.IO
open System.Collections

let f = File.ReadAllLines("../../../input9.txt")
let instr =
    f
    |> Array.map (fun s -> 
        let c = s.Split(' ')
        (c[0], c[1] |> int)
    )

let gridSize = 1000
let visited: bool[][]  = Array.zeroCreate gridSize
for i in 0..gridSize-1 do
    visited[i] <- Array.zeroCreate gridSize
    for j in 0..gridSize-1 do
        visited[i][j] <- false


type Pos =
    { x: int
      y: int
    }

let mutable knots: Pos array = Array.zeroCreate 10
for i in 0..9 do
    knots[i] <- { x = gridSize/2; y = gridSize/2 }

//let mutable h: Pos = { x = gridSize/2; y = gridSize/2 }
//let mutable t: Pos = { x = gridSize/2; y = gridSize/2 }
visited[knots[0].x][knots[0].y] <- true

let computeNewTailPos (h: Pos) (t: Pos) : Pos =
    let dirY = if (h.y - t.y) > 0 then 1 else -1
    let dirX = if (h.x - t.x) > 0 then 1 else -1
    let distX = abs (h.y - t.y)
    let distY = abs (h.x - t.x)

    if h = t then
        // Don't move
        t
    elif h.x = t.x then
        // Update Y
        if distX <= 1 then t
        else { t with y = t.y + dirY }
    elif h.y = t.y then
        // Update X
        if distY <= 1 then t
        else { t with x = t.x + dirX }
    else
        if distX > 1 || distY > 1 then
            // Update diag
            { t with
                x = t.x + dirX
                y = t.y + dirY
            }
        else
            t

let countVisited () : int =
    // Count unique visited cells
    let mutable count = 0
    for i in 0..gridSize-1 do
        for j in 0..gridSize-1 do
            if visited[i][j] then count <- count + 1
    count

instr
|> Array.iter (fun (d, c) ->
    let dx =
        match d with
        | "R" -> 1
        | "L" -> -1
        | _ -> 0

    let dy =
        match d with
        | "U" -> -1
        | "D" -> 1
        | _ -> 0

    // Step 1 for each count
    for c1 in 1..c do
        // Update head knot first
        knots[0] <- { knots[0] with x = (knots[0].x + (dx * 1)); y = (knots[0].y + (dy * 1)) }

        // Have other knots follow in seq
        for k in 1..9 do    
            // Update tail and visited cell
            knots[k] <- computeNewTailPos knots[k-1] knots[k]

            if k = 9 then
                visited[knots[9].x][knots[9].y] <- true

    //let count = countVisited ()
    //printfn "Count: %i" count
)


let count = countVisited ()
printfn "%i" count

*)
