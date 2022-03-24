module SwaggerEndpointDefinition

open System.Threading.Tasks

open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.DependencyInjection

[<Sealed>]
type SwaggerEndpointDefinitions() =
    interface IEndpointDefinition with
        member _.DefineServices(appBuilder) =
            appBuilder.Services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen()
            |> ignore

        member _.DefineEndpoints(app) =
            app.MapGet("/", fun (context: HttpContext) ->
                context.Response.Redirect("/swagger")
                Task.CompletedTask
                ) |> ignore

            app
                .UseSwagger()
                .UseSwaggerUI() |> ignore
