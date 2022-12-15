module Problem15


open System.IO

let f = File.ReadAllLines("../../../input15.txt")

type Pos = 
    { x: int
      y: int
    }

// (sensor, beacon)
let sb =
    f
    |> Array.map (fun s ->
        let s2 = s.Split(':')

        let s3 = s2[0].Split(',')
        let sP =
            { x = (s3[0].Substring(12)) |> int
              y = (s3[1].Substring(3)) |> int
            }

        let s4 = s2[1].Split(',')
        let bP =
            { x = (s4[0].Substring(24)) |> int
              y = (s4[1].Substring(3)) |> int
            }

        (sP, bP)
    )

// Manhatten dist
let getDistance (a: Pos) (b: Pos) =
    abs(a.x - b.x) + abs(a.y - b.y)

//printfn "%A" sb

let distances = 
    sb
    |> Array.map (fun (s, b) ->
        (s, getDistance s b)
    )

//
let checkCollision (p: Pos) ((c, r): (Pos * int)) : bool =
    getDistance p c <= r

//
let getRowCoverage (row: int) (len: int) : int =
    // Run through each X value of the row and determine
    // point collision in circle of (S, dist)

    let mutable count = 0
    for x in -1_000_000..len-1 do
    //for x in 0..4_000_000 do
        let mutable collisionHit = false
        let mutable collisionHitWithBeacon = false

        distances
        |> Array.indexed
        |> Array.iter (fun (i, (s, d)) ->
            let p = { x = x; y = row }

            // Check direct collision with beacon first and then empty space
            if p = (snd sb[i]) then
                collisionHitWithBeacon <- true
            elif checkCollision p (s, d) then
                collisionHit <- true
        )

        if collisionHit then count <- count + 1

        
        //if collisionHitWithBeacon then printf "B"
        //else if collisionHit then printf "%s" "#" else printf "%s" "."

    printfn ""

    count

//let soln1 = getRowCoverage 2_000_000 7_000_000
//printfn "%A" soln1


// Soln2, must be on the edge of a sensor circle, generate points on the outside
let candidates =
    distances
    |> Array.map (fun (s, d) ->
        let outerR = d + 1

        let mutable points = []

        // Span out NW, NE, SE, SW diagnoals (len of each matches outer radius)
        for nw in 0..outerR do
            points <- { x = s.x - (outerR - nw); y = s.y - nw} :: points
        for ne in 0..outerR do
            points <- { x = s.x + ne; y = s.y - (outerR - ne)} :: points
        for se in 0..outerR do
            points <- { x = s.x + (outerR - se); y = s.y + se } :: points
        for sw in 0..outerR do
            points <- { x = s.x - sw; y = s.y + (outerR - sw)} :: points

        let g =
            points
            |> List.filter (fun p ->
                p.x <= 4_000_000 && p.y <= 4_000_000 && p.x >=0 && p.y >=0 
            )

        printfn "Generated: %A" (g |> List.length)
        g
    )
    |> List.concat
    //|> List.distinct
    |> List.toArray

printfn "Candidate points: %i" (candidates |> Array.length)

//
let getOuterCoverage (points: Pos array) =
    points
    |> Array.iter (fun p ->
        let mutable hit = false

        distances
        |> Array.iter (fun (s, d) ->
            // Check direct collision with beacon first and then empty space
            if (checkCollision p (s, d)) then
                hit <- true
        )

        if not hit then
            printfn "S2: %A" (p.x * 4_000_000 + p.y)
    )

getOuterCoverage candidates