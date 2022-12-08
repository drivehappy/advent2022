open System.IO
open System.Collections

let f = File.ReadAllLines("../../../input7.txt")

type Dir =
    {
        name: string
        parent: Dir option
        mutable children: Dir list
        mutable filesizes: int //list
        mutable totalsize: int
    }

let mutable rootTree: Dir = { name = "/"; parent = None; children = []; filesizes = 0; totalsize = 0 }
let mutable currDir = rootTree

for f2 in f do
    let v = f2.Split(' ')
    if v[0] = "$" then
        if v[1] = "cd" then
            if v[2] = "/" then
                currDir <- rootTree
            elif v[2] = ".." then
                currDir <- currDir.parent.Value
            else
                currDir.children
                |> List.tryFind (fun x -> x.name = v[2])
                |> function
                    | None ->
                        // Not yet found, add it
                        let newDir: Dir = { name = v[0]; parent = Some currDir; children = []; filesizes = 0; totalsize = 0 }
                        currDir.children <- newDir :: currDir.children
                        currDir <- newDir
                    | Some c -> currDir <- c
        elif v[1] = "ls" then
            ()
    elif v[0] = "dir" then
        let newDir: Dir = { name = v[0]; parent = Some currDir; children = []; filesizes = 0; totalsize = 0 }
        currDir.children <- newDir :: currDir.children
    else
        // ls of file size, don't care about filename, just size
        let filesize = v[0]
        currDir.filesizes <- (v[0] |> int) + currDir.filesizes


// Update entire tree with summed dir sizes
let rec updateDirSize (t : Dir) : int =
    let mutable sumDir = 0

    sumDir <-
        (
            t.children
            |> List.sumBy updateDirSize
        )

    sumDir <-
        sumDir + t.filesizes
    
    t.totalsize <- sumDir
    sumDir

let _ = updateDirSize rootTree


(* Solution 1
let rec sumFiltered (t : Dir) : int =
    let c =
        if t.totalsize <= 100_000 then t.totalsize
        else 0

    let c2 =
        t.children
        |> List.sumBy sumFiltered

    c + c2

printfn "%A" (sumFiltered rootTree)
*)


// Soln 2
let totalConsumed = rootTree.totalsize
let target = 30_000_000 - ((70_000_000) - totalConsumed)

// Find the smallest single directory size that is above target

let rec flattenDirs (t : Dir) : Dir list =
    t :: ((t.children |> List.map flattenDirs) |> List.concat)

let flat = flattenDirs rootTree
printfn "%i" (flat |> List.length)

let g =
    flat
    |> List.map (fun x -> x.totalsize)
    |> List.filter (fun x -> x >= target)
    |> List.sort

printfn "%A" g

let smallestTotal =
    g
    |> List.head

printfn "%A" totalConsumed
printfn "%A" target
printfn "%A" smallestTotal