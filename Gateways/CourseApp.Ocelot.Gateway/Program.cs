using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var configuration = new ConfigurationBuilder()
                            .AddJsonFile($"configuration.{app.Environment.EnvironmentName.ToLower()}.json")
                            .AddEnvironmentVariables()
                            .Build();

builder.Services.AddOcelot(configuration);


if (app.Environment.IsProduction())
{
    
}

app.UseOcelot().Wait();

app.Run();
