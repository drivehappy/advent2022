open System.IO

let f = File.ReadAllLines("../../../input.txt")

let elves = Array.zeroCreate 10000
let mutable count = 0
let mutable index = 0

for l in f do
    if l.Length = 0 then
        elves[index] <- count
        index <- index + 1
        count <- 0
    else
        count <- count + (int l)

let c =
    elves
    |> Array.sortDescending

printfn "%i" (c[0] + c[1] + c[2])