using Inforce.Server.Domain.DTOs;
using Inforce.Server.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Inforce.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto request)
    {
        bool isRegistered = await _authService.RegisterAsync(request.Username, request.Password);

        if (!isRegistered)
            return BadRequest("User already exists");

        return Ok("Register success");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto request)
    {
        var token = await _authService.AuthenticateAsync(request.Username, request.Password);
        if (token == null)
            return Unauthorized("Wrong login or password");

        return Ok(new { Token = token });
    }

    [HttpGet("isAdmin")]
    public IActionResult IsAdmin()
    {
        var isAdmin = User.IsInRole("Admin");
        return Ok(new { isAdmin });
    }

    [HttpGet("getRole")]
    public IActionResult GetRole()
    {
        var role = User.FindFirstValue(ClaimTypes.Role);
        if (role == null)
        {
            return Unauthorized();
        }
        return Ok(new { role });
    }
}
