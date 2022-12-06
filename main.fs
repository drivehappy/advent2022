open System.IO

let f = File.ReadAllLines("../../../input.txt")

let c = f[0].ToCharArray()

let g =
    c
    |> Array.windowed 14
    |> Array.indexed
    |> Array.map (fun (i, x) ->
        if (x |> Array.distinct |> Array.length = 14) then
            printfn "Index: %i" (i + 14)
            System.Environment.Exit(0)
    )
