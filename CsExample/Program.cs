using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
builder.AddEndpointDefinitions();

var app = builder.Build();
app.UseEndpointDefinitions();
app.Run();
