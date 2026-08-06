using Microsoft.EntityFrameworkCore;
using StudentManagementApi.Data;
using StudentManagementApi.Interfaces;
using StudentManagementApi.Services;

var builder = WebApplication.CreateBuilder(args);

//register services
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

builder.Services.AddScoped<IStudentService, StudentService>();

//Swagger/OpenAPi
builder.Services.AddOpenApi();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); 
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

