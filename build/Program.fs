open System
open System.IO
open Fake.IO
open Fake.Core

let path xs = Path.Combine(Array.ofList xs)

let solutionRoot = Files.findParent __SOURCE_DIRECTORY__ "Feliz.ViewEngine.Htmx.sln";

let src = path [ solutionRoot; "src" ]

let dotnet args dir msg =
    if Shell.Exec(Tools.dotnet, args, dir) <> 0
    then failwith msg

let publish() =
    Shell.deleteDir (path [ src; "bin" ])
    Shell.deleteDir (path [ src; "obj" ])

    dotnet "pack -c Release" src "Packing the library failed"

    let nugetKey =
        match Environment.environVarOrNone "NUGET_KEY" with
        | Some nugetKey -> nugetKey
        | None -> failwith "The Nuget API key must be set in a NUGET_KEY environmental variable"

    let nugetPath =
        Directory.GetFiles(path [ src; "bin"; "Release" ])
        |> Seq.head
        |> Path.GetFullPath

    dotnet (sprintf "nuget push %s -s nuget.org -k %s" nugetPath nugetKey) src "Pushing the library to nuget failed"

[<EntryPoint>]
let main argv =
    match argv with
    | [| "build" |] -> dotnet "build -c Release" solutionRoot "Building solution failed"
    | [| "publish" |] -> publish()
    | _ -> ()
    0