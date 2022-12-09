namespace prob2

(*
open System.IO

let f = File.ReadAllLines("../../../input2.txt")

let d =
    f
    |> Array.splitAt(1)

// A | X = Rock
// B | Y = Paper
// C | Z = Scissors

type Type =
    | Rock
    | Paper
    | Scissors

type Outcome =
    | Win
    | Lose
    | Draw

let otherToType (c: char) : Type =
    match c with
    | 'A' -> Rock
    | 'B' -> Paper
    | 'C' -> Scissors

let meToType (c: char) : Type =
    match c with
    | 'X' -> Rock
    | 'Y' -> Paper
    | 'Z' -> Scissors

let meToOutcome (c: char) : Outcome =
    match c with
    | 'X' -> Lose
    | 'Y' -> Draw
    | 'Z' -> Win

let calcScore (other: Type) (me: Type) =
    let s1 =
        if other = me then
            3
        else
            match (other, me) with
            | (Rock, Paper) -> 6
            | (Rock, Scissors) -> 0
            | (Paper, Rock) -> 0
            | (Paper, Scissors) -> 6
            | (Scissors, Rock) -> 6
            | (Scissors, Paper) -> 0

    let s2 =
        match me with
        | Rock -> 1
        | Paper -> 2
        | Scissors -> 3

    s1 + s2
    
let calcScore2 (other: Type) (me: Outcome) =
    let myPlay =
        if me = Draw then
            other
        else
            match (other, me) with
            | (Rock, Win) -> Paper
            | (Rock, Lose) -> Scissors
            | (Paper, Win) -> Scissors
            | (Paper, Lose) -> Rock
            | (Scissors, Win) -> Rock
            | (Scissors, Lose) -> Paper

    let s1 =
        if other = myPlay then
            3
        else
            match (other, myPlay) with
            | (Rock, Paper) -> 6
            | (Rock, Scissors) -> 0
            | (Paper, Rock) -> 0
            | (Paper, Scissors) -> 6
            | (Scissors, Rock) -> 6
            | (Scissors, Paper) -> 0

    let s2 =
        match myPlay with
        | Rock -> 1
        | Paper -> 2
        | Scissors -> 3

    s1 + s2
*)

(* Soln 1
let e =
    f
    |> Array.sumBy (fun a ->
        let b = a.Split(' ')
        let (d, e) = (otherToType (b[0][0]), meToType (b[1][0]))

        calcScore d e
    )

printfn "%i" e
*)

(* Soln 2
let e =
    f
    |> Array.sumBy (fun a ->
        let b = a.Split(' ')
        let (d, e) = (otherToType (b[0][0]), meToOutcome (b[1][0]))

        calcScore2 d e
    )

printfn "%i" e
*)