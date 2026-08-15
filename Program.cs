using Microsoft.EntityFrameworkCore;
using StudentManagementApi.Data;
using StudentManagementApi.Interfaces;
using StudentManagementApi.Services;
using DotNetEnv;
using StudentManagementApi.Middleware;
var builder = WebApplication.CreateBuilder(args);

Env.Load();

var testConnection =
    Environment.GetEnvironmentVariable(
        "ConnectionStrings__DefaultConnection"
    );

Console.WriteLine(
    $"Connection string loaded: {!string.IsNullOrWhiteSpace(testConnection)}"
);

builder.Services.AddControllers();

var connectionString =
    Environment.GetEnvironmentVariable(
        "ConnectionStrings__DefaultConnection"
    );

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "Database connection string is not configured."
    );
}
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();