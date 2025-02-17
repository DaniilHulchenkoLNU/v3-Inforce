using Inforce.Server.Domain.DTOs;
using Inforce.Server.Services;
using Microsoft.AspNetCore.Mvc;

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
            return BadRequest("Пользователь уже существует.");

        return Ok("Регистрация успешна!");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto request)
    {
        var token = await _authService.AuthenticateAsync(request.Username, request.Password);
        if (token == null)
            return Unauthorized("Неверный логин или пароль.");

        return Ok(new { Token = token });
    }
}
