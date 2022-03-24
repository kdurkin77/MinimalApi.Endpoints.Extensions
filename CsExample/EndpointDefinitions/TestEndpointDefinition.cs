using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CsExample.EndpointDefinitions
{
    public class TestEndpointDefinition : IEndpointDefinition
    {
        public void DefineServices(WebApplicationBuilder appBuilder) { }

        public void DefineEndpoints(WebApplication app)
        {
            static string SayHello() => "Hello";
            static string SayHelloTo([FromBody] string name) => "Hello, " + name;

            app.MapGet("/Test", SayHello).WithTags("Test");
            app.MapPost("/Test", SayHelloTo).WithTags("Test");
        }
    }
}
