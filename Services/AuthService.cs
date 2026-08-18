using Microsoft.EntityFrameworkCore;
using StudentManagementApi.Data;
using StudentManagementApi.DTOs;
using StudentManagementApi.Exceptions;
using StudentManagementApi.Models;

namespace StudentManagementApi.Services;

public class AuthService
{
    private readonly AppDbContext context;
    private readonly ILogger<AuthService> logger;

    public AuthService(AppDbContext context,  ILogger<AuthService> logger)
    {
        this.context = context;
        this.logger = logger;
    }

    public async Task<User> Register(RegisterDto dto)
    {
        logger.LogInformation("Registering new user");
        var existingUser = await context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (existingUser != null)
        {
            throw new UserAlreadyExistsException("User already exists");
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = passwordHash,
            Role = "Student"
        };
        
        context.Users.Add(user);
        await context.SaveChangesAsync();
        logger.LogInformation("Registered new user");
        return user;
    }
}

