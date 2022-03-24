module TestEndpointDefinition

open System

open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http


[<CLIMutable>]
type TestParams = {
    Name: string
}

[<Sealed>]
type TestEndpointDefinitions() =

    let sayHello () = "Hello"
    let sayHelloToString name = sprintf "Hello, %s" name
    let sayHelloTo testParams = sayHelloToString testParams.Name

    interface IEndpointDefinition with
        member _.DefineServices(_) =
            ()

        member this.DefineEndpoints(app) =
            app.MapGet("/Test", Func<_> sayHello).WithTags("Test") |> ignore
            app.MapPost("/Test", Func<_> sayHello).WithTags("Test") |> ignore

            app.MapGet("/TestWithQuery", Func<_,_> (fun name -> sayHelloToString name)).WithTags("TestWithQuery") |> ignore
            app.MapPost("/TestWithQuery", Func<_,_> (fun name -> sayHelloToString name)).WithTags("TestWithQuery") |> ignore

            app.MapPost("/TestWithBody", Func<_,_> sayHelloTo).WithTags("TestWithBody") |> ignore
