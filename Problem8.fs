namespace Problem8

(*
open System.IO
open System.Collections

let f = File.ReadAllLines("../../../input8.txt")

let treeGrid =
    f
    |> Array.map (fun s -> (s.ToCharArray() |> Array.map int))

let width = treeGrid[0].Length
let height = treeGrid.Length

let mutable treeCount: bool array array =
    let a = Array.zeroCreate height

    a
    |> Array.map (fun _ -> Array.zeroCreate width)
*)
(*
// Left
let left =
    let mutable c = 0
    for j in 0..height-1 do
        let mutable lastMaxVal = -1
        for i in 0..width-1 do
            if treeGrid[i][j] > lastMaxVal then
                lastMaxVal <- treeGrid[i][j]
                if not (treeCount[i][j]) then
                    treeCount[i][j] <- true
                    c <- c + 1
    c

let right =
    let mutable c = 0
    for j in 0..height-1 do
        let mutable lastMaxVal = -1
        for i in width-1..-1..0 do
            if treeGrid[i][j] > lastMaxVal then
                lastMaxVal <- treeGrid[i][j]
                if not (treeCount[i][j]) then
                    treeCount[i][j] <- true
                    c <- c + 1
    c

let top =
    let mutable c = 0
    for i in 0..width-1 do
        let mutable lastMaxVal = -1
        for j in 0..height-1 do    
            if treeGrid[i][j] > lastMaxVal then
                lastMaxVal <- treeGrid[i][j]
                if not (treeCount[i][j]) then
                    treeCount[i][j] <- true
                    c <- c + 1
    c

let bot =
    let mutable c = 0
    for i in 0..width-1 do
        let mutable lastMaxVal = -1
        for j in height-1..-1..0 do    
            if treeGrid[i][j] > lastMaxVal then
                lastMaxVal <- treeGrid[i][j]
                if not (treeCount[i][j]) then
                    treeCount[i][j] <- true
                    c <- c + 1
    c

printfn "%i" left
printfn "%i" right
printfn "%i" top
printfn "%i" bot
printfn "%i" (left + right + top + bot)
*)

(*
let scenicScore (x: int) (y: int) : int =
    let h = treeGrid[x][y]

    let left =
        let mutable score = 0
        let mutable exit = false
        for i in x-1..-1..0 do
            if not exit then
                if treeGrid[i][y] >= h then
                    score <- score + 1
                    exit <- true
                else score <- score + 1
        score

    let right =
        let mutable score = 0
        let mutable exit = false
        for i in x+1..width-1 do
            if not exit then
                if treeGrid[i][y] >= h then
                    score <- score + 1
                    exit <- true
                else score <- score + 1
        score

    let up =
        let mutable score = 0
        let mutable exit = false
        for j in y-1..-1..0 do
            if not exit then
                if treeGrid[x][j] >= h then
                    score <- score + 1
                    exit <- true
                else score <- score + 1
        score

    let down =
        let mutable score = 0
        let mutable exit = false
        for j in y+1..height-1 do
            if not exit then
                if treeGrid[x][j] >= h then
                    score <- score + 1
                    exit <- true
                else score <- score + 1
        score

    left * right * up * down


let mutable high = 0
for i in 0..width-1 do
    for j in 0..height-1 do
        let s = scenicScore i j
        if s > high then high <- s

printfn "%i" high
*)