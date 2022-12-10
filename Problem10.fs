namespace prob10

(*
open System.IO
open System.Collections

let f = File.ReadAllLines("../../../input10.txt")

type Op =
    | Noop
    | Addx of int

let ops =
    f
    |> Array.map (fun x ->
        match x with
        | "noop" -> Noop
        | s ->
            let s1 = s.Split(' ')
            Addx (s1[1] |> int)
    )


let mutable cycles = [ 20; 60; 100; 140; 180; 220 ]


let mutable cycle = 0
let mutable str: int list = []
let mutable extraCycle = false
let mutable gx = 1
let mutable nextX = 0

let mutable pixels: bool array = Array.zeroCreate (40 * 6)
let mutable pixelIndex = 0


let check() =
    match cycles with
    | (h :: t) ->
        if h = cycle then
            let s = (cycle * gx)
            str <- s :: str
            cycles <- t
            printfn "%i %i %i" cycle gx s
            printfn "%A" cycles
    | [] -> ()


// 1
ops
|> Array.iter (fun op ->
    match op with
    | Noop -> ()
    | Addx x ->
        nextX <- x
        extraCycle <- true

    cycle <- cycle + 1
    //check()
    
    // Draw
    let m = pixelIndex % 40
    if m = (gx-1) || m = gx || m = (gx+1) then
        pixels[pixelIndex] <- true
    pixelIndex <- pixelIndex + 1

    //
    if extraCycle then

        // Draw
        let m = pixelIndex % 40
        if m = (gx-1) || m = gx || m = (gx+1) then
            pixels[pixelIndex] <- true
        pixelIndex <- pixelIndex + 1

        cycle <- cycle + 1 
        //check()

        gx <- gx + nextX
        extraCycle <- false
        

)

//let s = str |> List.sum
//printfn "%i" s

// 2
for y in 0..5 do
    for x in 0..39 do
        if pixels[(y * 40) + x] then
            printf "#"
        else
            printf "."
    printfn ""
*)