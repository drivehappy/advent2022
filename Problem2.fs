namespace prob2

(*
module main

open System.IO

let f = File.ReadAllLines("../../../input2.txt")

let d =
    f
    |> Array.splitAt(1)

let e =
    f
    |> Array.map (fun a ->
        let b = a.Split(' ')
        let (d, e) = (b[0][0], b[1][0])

        if d = (e - 'X') then
            3
        elif d = (e - 'Y') then
            

        if a[0] == 'c' then
            0
        else
            0
    )

*)