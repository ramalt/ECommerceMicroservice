using CourseApp.Catalog.Api.Config;
using CourseApp.Catalog.Api.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Program));

// Db Connection configs
builder.Services.AddSingleton<IDatabaseSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value);

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

// Category Service registration
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICourseService, CourseService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
