namespace Prob3

(*
open System.IO

let f = File.ReadAllLines("../../../input3.txt")


let priority (c: char) =
    if c >= 'a' && c <= 'z' then
        (int (c - 'a')) + 1
    else
        (int (c - 'A')) + 27


let items =
    f
    |> Array.map (fun x -> (x.Substring(0, x.Length/2).ToCharArray(), x.Substring(x.Length/2).ToCharArray()))

let identical =
    items
    |> Array.map (fun (a, b) ->
        let mutable a1 = Set.empty
        a |> Array.iter (fun c -> a1 <- a1.Add(c))

        let mutable b1 = Set.empty
        b |> Array.iter (fun c -> b1 <- b1.Add(c))

        let c1 = Set.intersect a1 b1
        assert(c1.Count = 1)
        (c1 |> Set.toArray)[0]
    )

let score =
    identical
    |> Array.sumBy priority

printfn "%A" score


let groups =
    f
    |> Array.chunkBySize 3

let g =
    groups
    |> Array.map (fun x ->
        assert(x |> Array.length = 3)

        let a = x[0].ToCharArray()
        let b = x[1].ToCharArray()
        let c = x[2].ToCharArray()

        let mutable a1 = Set.empty
        a |> Array.iter (fun c -> a1 <- a1.Add(c))

        let mutable b1 = Set.empty
        b |> Array.iter (fun c -> b1 <- b1.Add(c))

        let mutable c1 = Set.empty
        c |> Array.iter (fun c -> c1 <- c1.Add(c))

        let h = Set.intersectMany (seq { a1; b1; c1 }) |> Set.toArray
        assert(h.Length = 1)

        h[0]
    )
    |> Array.sumBy priority

printfn "%i" g
*)