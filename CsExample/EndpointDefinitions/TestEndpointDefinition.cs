using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CsExample.EndpointDefinitions
{
    public class TestParams
    {
        public string? Name { get; set; }
    }

    public class TestEndpointDefinition : IEndpointDefinition
    {
        public void DefineServices(WebApplicationBuilder appBuilder) { }

        public void DefineEndpoints(WebApplication app)
        {
            static string SayHello() => "Hello";
            static string SayHelloTo(string name) => "Hello, " + name;
            static string SayHelloToBody([FromBody] string name) => SayHelloTo(name);
            static string SayHelloToBodyObject([FromBody] TestParams testParams) => SayHelloTo(testParams.Name ?? string.Empty);

            app.MapGet("/Test", SayHello).WithTags("Test");
            app.MapPost("/Test", SayHello).WithTags("Test");

            app.MapGet("/TestWithQuery", SayHelloTo).WithTags("TestWithQuery");
            app.MapPost("/TestWithQuery", SayHelloTo).WithTags("TestWithQuery");

            app.MapPost("/TestWithBody", SayHelloToBody).WithTags("TestWithBody");

            app.MapPost("/TestWithBodyObject", SayHelloToBodyObject).WithTags("TestWithBodyObject");
        }
    }
}
