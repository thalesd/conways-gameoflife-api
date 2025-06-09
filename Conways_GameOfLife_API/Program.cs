using Conways_GameOfLife_API.Configuration;
using Conways_GameOfLife_API.Data;
using Conways_GameOfLife_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<APIConfig>(config =>
{
    var maxIter = Environment.GetEnvironmentVariable("API_MAX_ITERATIONS");

    if (!string.IsNullOrEmpty(maxIter) && int.TryParse(maxIter, out var max))
        config.MaxIterations = max;
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Information);

builder.Services.AddSingleton<InMemoryBoardStore>();
builder.Services.AddSingleton<GameOfLifeService>();
builder.Services.AddSingleton<BoardService>();
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
