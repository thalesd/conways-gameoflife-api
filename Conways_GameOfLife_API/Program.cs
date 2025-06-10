using Conways_GameOfLife_API.Configuration;
using Conways_GameOfLife_API.Data;
using Conways_GameOfLife_API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<APIConfig>(config =>
{
    config.MaxIterations = int.TryParse(Environment.GetEnvironmentVariable("API_MAX_ITERATIONS"), out var max) ? max : 1000;

    config.DataSource = Environment.GetEnvironmentVariable("DB_DATA_SOURCE") ?? "Data Source=gameOfLife.db";
});

builder.Services.AddDbContext<GameOfLifeDbContext>((service, builder) =>
    {
        var options = service.GetRequiredService<IOptions<APIConfig>>().Value;

        builder.UseSqlite(options.DataSource);
    }
);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Information);

builder.Services.AddScoped<BoardRepository>();
builder.Services.AddScoped<BoardService>();

builder.Services.AddSingleton<GameOfLifeService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
