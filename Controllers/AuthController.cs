using Microsoft.AspNetCore.Mvc;
using StudentManagementApi.DTOs;
using StudentManagementApi.Services;

namespace StudentManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController: ControllerBase
{
    private readonly AuthService authService;
    public AuthController(AuthService authService)
    {
        this.authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var user = await authService.Register(dto);
        
        return Ok(new
        {
            message = "User was successfully registered.",
            userId = user.Id,
            fullName = user.FullName,
            email = user.Email,
            role = user.Role
        });
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var token = await authService.Login(dto);

        return Ok(new
        {
            message = "Login successful",
            token = token
        });
    }
    
}