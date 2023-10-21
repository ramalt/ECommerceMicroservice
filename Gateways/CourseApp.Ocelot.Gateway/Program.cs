using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;


var builder = WebApplication.CreateBuilder(args);



IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile($"configuration.{builder.Environment.EnvironmentName.ToLower()}.json")
                            .Build();


builder.Services.AddOcelot(configuration);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer("GatewayAuthScheme",options =>
{
    options.Authority = builder.Configuration["IdentityServerURL"];
    options.Audience = builder.Configuration["Audience"];
    options.RequireHttpsMetadata = true;

});

    

var app = builder.Build();

app.UseOcelot().Wait();


app.Run();

