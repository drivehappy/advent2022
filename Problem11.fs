module Problem11

open System.IO

// !!!!
let monkeyCount = 7
let f = File.ReadAllLines("../../../input11.txt")
//


let mutable monkeyItems: int64 list array = Array.zeroCreate (monkeyCount + 1)
for i in 0..monkeyCount do
    monkeyItems[i] <- []

// Parse initial items
let mutable line = 0
let mutable index = 0
for i in 0..monkeyCount do
    let items =
        f[line+1].Substring(18).Split(',')
        |> Array.map (fun s -> s.Trim() |> int64)
        |> Array.toList

    monkeyItems[index] <- items
    printfn "Items[%i]: %A" index monkeyItems[index]

    index <- index + 1
    line <- line + 7


// Old -> New
let monkeyOp (monkey: int) (old: int64) : int64 =
    match monkey with
    | 0 -> old * 7L
    | 1 -> old + 8L
    | 2 -> old * 13L
    | 3 -> old + 7L
    | 4 -> old + 2L
    | 5 -> old + 1L
    | 6 -> old + 4L
    | 7 ->
        let n = old * old
        if n > 9699690L then
            //printfn "DEBUG: %A" n
            n % 9699690L
        else n
    | _ -> assert(false); 0


(*
let monkeyOp (monkey: int) (old: WorryLevel) : WorryLevel =
    match monkey with
    | 0 -> { old with worry = old.worry * 19L }
    | 1 -> { old with worry = old.worry + 6L }
    | 2 -> 
        let n = old.worry * old.worry
        (*
        if n % 96577L = 0 then
            printfn "DEBUG: %A" n
            { worry = n / 96577L; multiplier = old.multiplier + 1L }
        *)
        if n > 96577L then
            //printfn "DEBUG: %A" n
            { worry = n % 96577L; multiplier = old.multiplier + 1L }
        else { old with worry = n }
    | 3 -> { old with worry = old.worry + 3L }
    | _ -> assert(false); { old with worry = 0 }
*)

// Test: item -> monkeyIndex
let monkeyTest (srcMonkey: int) (worry: int64) : int =
    match srcMonkey with
    | 0 -> if worry % 17L = 0L then 5 else 3
    | 1 -> if worry % 2L = 0L then 7 else 6
    | 2 -> if worry % 5L = 0L then 1 else 6
    | 3 -> if worry % 3L = 0L then 5 else 2
    | 4 -> if worry % 7L = 0L then 0 else 3
    | 5 -> if worry % 13L = 0L then 2 else 1
    | 6 -> if worry % 19L = 0L then 7 else 4
    | 7 -> if worry % 11L = 0L then 0 else 4
    | _ -> assert(false); 0


(*
let monkeyTest (srcMonkey: int) (worry: WorryLevel) : int =
    match srcMonkey with
    | 0 -> if worry.worry % 23L = 0 then 2 else 3
    | 1 -> if worry.worry % 19L = 0 then 2 else 0
    | 2 -> if worry.worry % 13L = 0 then 1 else 3
    | 3 -> if worry.worry % 17L = 0 then 0 else 1
    | _ -> assert(false); 0
*)

//
let mutable monkeyInspect: int array = Array.zeroCreate (monkeyCount + 1)
for i in 0..monkeyCount do
    monkeyInspect[i] <- 0

for round in 1..10000 do
    //printfn "Round %i" round

    for monkeyIndex in 0..monkeyCount do
        //printfn "monkeyIndex: %A" monkeyIndex

        monkeyItems[monkeyIndex]
        |> List.iter (fun worryItem ->
            monkeyInspect[monkeyIndex] <- monkeyInspect[monkeyIndex] + 1

            //printfn "  worryItem: %A" worryItem
            let worryLevel = monkeyOp monkeyIndex worryItem
            //let boredLevel: int64 = worryLevel / 3L

            let boredLevel: int64 =
                (*
                if worryLevel.worry % 96577L = 0 then
                    printfn "DEBUG: %A" worryLevel.worry
                    { worryLevel with worry = worryLevel.worry / 96577L; multiplier = worryLevel.multiplier + 1L }
                *)
                if worryLevel > 9699690L then
                    worryLevel % 9699690L
                else worryLevel

            let newMonkey = monkeyTest monkeyIndex boredLevel
            //printfn "  Throw: %i -> %i (%i -> %i)" monkeyIndex newMonkey worryItem boredLevel

            // Add item
            monkeyItems[newMonkey] <- (boredLevel :: (monkeyItems[newMonkey] |> List.rev)) |> List.rev
        )

        // Clear old monkey items
        monkeyItems[monkeyIndex] <- []


//printfn "Items: %A" monkeyItems

let s = monkeyInspect |> Array.sortDescending
printfn "Results: %A" s
printfn "Soln1: %A" ((bigint s[0]) * (bigint s[1]))
