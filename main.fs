module main

open System.IO

let f = File.ReadAllLines("../../../input10.txt")

//
let (|Noop|_|) (s: string) =
    if s.Split(' ')[0] = "noop" then Some () else None

let (|Addx|_|) (s: string) =
    let s2 = s.Split(' ')
    if s2[0] = "addx" then Some (s2[1] |> int) else None
   
// Build the values at each cycle index, accumulating our x register as we go
let cycleValues =
    f
    |> Array.fold (fun (s, x) op ->
        match op with
        | Noop ->
            (x :: s, x)
        | Addx x2 ->
            let s' = x :: x :: s
            (s', x + x2)
    ) ([], 1)
    |> fst
    |> List.rev
    |> List.indexed
    |> List.map (fun (i, x) -> (i+1, x))

// Solution 1
let cycleIndicies = [ 20; 60; 100; 140; 180; 220 ]

cycleValues
|> List.filter (fun (i, _) -> cycleIndicies |> List.contains i)
|> List.sumBy (fun (i, x) -> i * x)
|> printfn "Soln1: %i"


// Solution 2
printfn "Soln2:"
cycleValues
|> List.iter (fun (i, x) ->
    let m = (i-1) % 40
    if m = 0 && i > 1 then printfn ""
    printf (if m = (x-1) || m = x || m = (x+1) then "#" else ".")
)
