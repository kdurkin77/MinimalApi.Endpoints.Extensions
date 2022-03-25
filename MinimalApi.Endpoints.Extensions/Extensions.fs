namespace Microsoft.AspNetCore.Builder

open System
open System.Reflection
open System.Runtime.CompilerServices

open Microsoft.Extensions.DependencyInjection


type IEndpointDefinition =
    abstract DefineServices  : WebApplicationBuilder -> unit
    abstract DefineEndpoints : WebApplication        -> unit


[<Extension; Sealed; AbstractClass; >]
type WebApplicationBuilderExtensions =

    [<Extension>]
    static member AddEndpointDefinitions(appBuilder: WebApplicationBuilder, assemblies: Assembly seq) =
        if isNull      appBuilder then nullArg    (nameof appBuilder)
        if isNull      assemblies then nullArg    (nameof assemblies)
        if Seq.isEmpty assemblies then invalidArg (nameof assemblies) (sprintf "%s cannot be empty" (nameof assemblies))

        let endpointDefs =
            let endpointType = typeof<IEndpointDefinition>
            assemblies
            |> Seq.where   (not << isNull)
            |> Seq.collect (fun x -> x.GetTypes())
            |> Seq.where   (fun x -> endpointType.IsAssignableFrom(x) && not x.IsInterface && not x.IsAbstract)
            |> Seq.distinct
            |> Seq.map     (fun x -> Activator.CreateInstance(x) :?> IEndpointDefinition)
            |> List.ofSeq

        if not (List.isEmpty endpointDefs) then 
            for endpointDef in endpointDefs do
                endpointDef.DefineServices appBuilder

            appBuilder.Services.AddSingleton(endpointDefs) |> ignore

        appBuilder

    [<Extension>]
    static member AddEndpointDefinitions(appBuilder: WebApplicationBuilder, assembly: Assembly) =
        if isNull appBuilder then nullArg (nameof appBuilder)
        if isNull assembly   then nullArg (nameof assembly)

        appBuilder.AddEndpointDefinitions([ assembly ])

    [<Extension>]
    static member AddEndpointDefinitions(appBuilder: WebApplicationBuilder) =
        if isNull appBuilder then nullArg (nameof appBuilder)

        AppDomain.CurrentDomain.GetAssemblies()
        |> List.ofSeq
        |> appBuilder.AddEndpointDefinitions


[<Extension; Sealed; AbstractClass;>]
type WebApplicationExtensions =

    [<Extension>]
    static member UseEndpointDefinitions(app: WebApplication) =
        if isNull app then nullArg (nameof app)

        app.Services.GetServices<IEndpointDefinition list>()
        |> Seq.concat
        |> Seq.iter (fun x -> x.DefineEndpoints(app))
        app
