using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace CsExample.EndpointDefinitions
{
    public sealed class SwaggerEndpointDefinition : IEndpointDefinition
    {
        public void DefineServices(WebApplicationBuilder appBuilder)
        {
            appBuilder.Services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen();
        }

        public void DefineEndpoints(WebApplication app)
        {
            app
                .UseSwagger()
                .UseSwaggerUI();

            app.MapGet("/", (HttpContext context) => {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            });
        }
    }
}
