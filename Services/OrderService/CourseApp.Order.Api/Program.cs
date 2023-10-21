using System.IdentityModel.Tokens.Jwt;
using CourseApp.Order.Application.Menu.Commands;
using CourseApp.Order.Infrastructure;
using CourseApp.Shared.services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

builder.Services.AddControllers(opt => {
    opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(CreateOrderCommandHandler).Assembly);

builder.Services.AddDbContext<OrderDbContext>(opt => {
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"), builder => {
        builder.MigrationsAssembly("CourseApp.Order.Infrastructure");
    });
});
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub"); //"sub" claim not mapping to "nameidentifier"

//Jwt config
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerURL"];
    options.Audience = builder.Configuration["Audience"];
    options.RequireHttpsMetadata = true;

});
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ISharedIdentityService, SharedIndentityService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
