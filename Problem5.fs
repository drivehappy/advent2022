namespace prob5

(*
open System.IO
open System.Collections

let f = File.ReadAllLines("../../../input5.txt")
let colCount = 9

let splitIndex =
    f
    |> Array.findIndex (fun x -> x.Trim().Length = 0)

let mutable stacks: Stack array = Array.zeroCreate colCount
for x in 0..colCount-1 do
    stacks[x] <- Stack()

// Parse crate letters
for i in splitIndex-1..-1..0 do
    let row = f[i].ToCharArray()

    // Parse row, we have 9
    for x in 0..4..((colCount-1)*4) do
        let col = x/4

        if row[x] = '[' then
            let r = row[x+1]
            stacks[col].Push(r)

// Instructions
for i in splitIndex+1..f.Length-1 do
    let index1 = f[i].IndexOf(" from ")
    let index2 = f[i].IndexOf(" to ")

    let (count, src, dst) =
        ( f[i].Substring(5, index1 - 5) |> int
        , f[i].Substring(index1 + 6, index2 - (index1 + 6)) |> int
        , f[i].Substring(index2 + 4) |> int
        )

    // Update stacks
    let tmpStack = Stack()
    for x in 1..count do
        let tmp = stacks[src-1].Pop()
        tmpStack.Push(tmp)
    for x in 1..count do 
        stacks[dst-1].Push(tmpStack.Pop())


// Soln 1
for i in 0..colCount-1 do
    printf "%A" (stacks[i].Peek())
*)