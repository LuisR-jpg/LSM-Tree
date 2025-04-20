using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Optional: Add Swagger if you're into OpenAPI docs
Console.WriteLine("Skipping this step because of missing dependencies");
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();

// Optional: Swagger UI during development
if (app.Environment.IsDevelopment())
{
    Console.WriteLine("Skipping this step because of missing dependencies");
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// --- Database setup ---
var db = new Database();

// --- Minimal API endpoints ---
// Health check
app.MapGet("/health", () =>
{
    return Results.Ok(new { status = "Healthy" });
});

// POST /create
app.MapPost("/create", (KeyValueDataTransferObject input) =>
{
    db.Create(input.Key, input.Value);
    return Results.Ok(new { message = "Key added (if it was not already there)." });
});

// GET /read/{key}
app.MapGet("/read/{key}", (long key) =>
{
    var value = db.Read(key);
    return value.HasValue
        ? Results.Ok(new { key, value = value.Value })
        : Results.NotFound(new { message = $"Key {key} not found." });
});

app.Run();
