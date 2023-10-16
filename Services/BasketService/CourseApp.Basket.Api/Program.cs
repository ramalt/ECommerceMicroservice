using CourseApp.Basket.Api;
using CourseApp.Basket.Api.Services;
using CourseApp.Basket.Api.Settings;
using CourseApp.Shared.services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// identiy service
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ISharedIdentityService, SharedIndentityService>();

//redis
builder.Services.configureRedis();

builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection("RegisSettings"));

builder.Services.AddScoped<IBasketService, BasketService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
