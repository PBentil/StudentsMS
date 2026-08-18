using Microsoft.EntityFrameworkCore;
using StudentManagementApi.Data;
using StudentManagementApi.Interfaces;
using StudentManagementApi.Services;
using DotNetEnv;
using StudentManagementApi.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


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

var jwtKey = builder.Configuration["JWT:KEY"];

if (string.IsNullOrWhiteSpace(jwtKey))
{
    throw new InvalidOperationException(
        "JWT key is not configured."
    );
}

builder.Services.AddAuthentication(
        JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,

            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtKey)
                ),

            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWT:ISSUER"],

            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:AUDIENCE"],

            ValidateLifetime = true
        };
    });

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

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();