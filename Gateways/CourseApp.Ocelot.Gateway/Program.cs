using Ocelot.DependencyInjection;
using Ocelot.Middleware;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile($"configuration.{builder.Environment.EnvironmentName.ToLower()}.json")
                            .Build();


builder.Services
    .AddOcelot(configuration);
    

var app = builder.Build();

app.UseOcelot().Wait();

app.Run();

