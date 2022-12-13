module main

open System.IO

let f = File.ReadAllLines("../../../input13.txt")

type PacketData =
    | Int of int
    | List of PacketData list
    | Empty


let rec parse (s: string) (j: int) : (PacketData * int) =
    //let chars = s.ToCharArray()

    let mutable level = 0
    let mutable data : int list = []
    
    let mutable i = j
    let mutable loop = true

    let packet =
        [
            while loop && i < s.Length do
                match s[i] with
                | '[' ->
                    let (p, ni) = parse s (i+1)
                    i <- ni-1
                    yield p
                | ']' ->
                    // Exit loop
                    loop <- false
                | ',' -> ()
                | c ->
                    let a = i
                    while i < s.Length && s[i] <> '[' && s[i] <> ']' && s[i] <> ',' do
                        i <- i + 1
                    i <- i-1

                    yield Int (int (s.Substring(a, i-a+1)))

                i <- i + 1
        ]
        |> List

    (packet, i)


let parsed =
    f
    |> Array.choose (fun x -> 
        if x.Length = 0 then None
        else parse (x.Substring(1, x.Length-2)) 0 |> fst |> Some
    )

(*
printfn "Parsed:"
parsed
|> Array.iter (fun p ->
    printfn "  %A" p
    printfn ""
)
*)

// Rules

type Ordering =
    | Ordered
    | NotOrdered
    | Unknown

let rec checkPacket (l: PacketData) (r: PacketData) : Ordering =
    match (l, r) with
    | (Empty, Empty) -> Unknown
    | (Empty, _) -> Ordered
    | (_, Empty) -> NotOrdered
    | (Int li, Int ri) -> if li < ri then Ordered elif li > ri then NotOrdered else Unknown
    | (List ll, List rl) ->
        if List.length ll = 0 && List.length rl = 0 then Unknown
        elif List.length ll = 0 then Ordered
        elif List.length rl = 0 then NotOrdered
        else
            // Adjust lengths
            let diff = List.length ll - List.length rl

            let newL =
                if diff < 0 then
                    [
                        for i in 1..abs(diff) do
                            yield Empty
                    ]
                    |> List.append ll
                else ll

            let newR =
                if diff > 0 then
                    [
                        for i in 1..abs(diff) do
                            yield Empty
                    ]
                    |> List.append rl
                else rl
              
            //
            let l = List.zip newL newR
            let lo =
                l
                |> List.map (fun (l, r) ->
                    //printfn "D1: %A %A" l r
                    let o = checkPacket l r
                    o
                )

            //printfn ""
            //printfn "%A" l
            //printfn "%A" lo

            let loo =
                lo
                |> List.fold (fun s t -> if s = Unknown then t else s) Unknown

            //printfn "%A" loo

            //if lo = Unknown then Ordered else lo
            loo
    | (Int li, List rl) -> checkPacket (List [Int li]) r
    | (List ll, Int ri) -> checkPacket l (List [Int ri])


let input =
    parsed
    |> Array.chunkBySize 2
    |> Array.map (fun p -> (p[0], p[1]))

let pairs =
    input
    |> Array.map (fun (l, r) ->
        //printfn ""

        let o = checkPacket l r
        //printfn "O: %A" o
        o
    )
    //|> Array.map (fun o -> if o = Unknown then Ordered else o)

(*
printfn "Order:"
pairs
|> Array.iter (fun p ->
    printfn "  %A" p
)
*)

let soln1 =
    pairs
    |> Array.indexed
    |> Array.filter (fun (i, o) ->
        o = Ordered
    )
    |> Array.sumBy (fun (i, _) -> i+1)

printfn "Soln1: %A" soln1


// Sort
let sorted =
    parsed
    |> Array.sortWith (fun l r ->
        if checkPacket l r = Ordered then -1
        else 1
    )

(*
printfn "S"
sorted
|> Array.iter (fun a -> printfn "  %A" a)
*)

let soln2 =
    sorted
    |> Array.indexed
    |> Array.choose (fun (i, p) ->
        if p = List [ List [ Int 6 ] ] || p = List [ List [ Int 2 ] ] then Some (i+1)
        else None
    )

printfn "%A" (soln2[0] * soln2[1])
