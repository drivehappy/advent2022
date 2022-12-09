namespace Prob4

(*
open System.IO

let f = File.ReadAllLines("../../../input4.txt")


let items =
    f
    |> Array.map (fun x ->
        let a = x.Split(',')
        let a1 = a[0].Split('-')
        let a2 = a[1].Split('-')

        ((int a1[0], int a1[1]), (int a2[0], int a2[1]))
    )

let g =
    items
    |> Array.sumBy (fun ((s1, e1), (s2, e2)) ->
        // Check full subset
        if s1 >= s2 && s1 <= e2 (*&& (e1 >= s2 && e1 <= e2)*) then 1
        elif s2 >= s1 && s2 <= e1 (*&& (e2 >= s1 && e2 <= e1)*) then 1
        else 0
    )

printfn "%i" g
*)