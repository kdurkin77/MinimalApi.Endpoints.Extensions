open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open System.Reflection

[<EntryPoint>]
let main args =
    let app =
        let builder = WebApplication.CreateBuilder(args)
        
        builder
            .AddEndpointDefinitions(Assembly.GetExecutingAssembly())
            .Build()

    app
        .UseEndpointDefinitions() 
        .UseHttpsRedirection()
        |> ignore

    app.Run()
    0

