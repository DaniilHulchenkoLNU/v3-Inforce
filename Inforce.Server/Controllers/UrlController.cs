using Inforce.Server.DAL.Interfaces;
using Inforce.Server.Domain;
using Inforce.Server.Domain.DTOs;
using Inforce.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Inforce.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UrlController : ControllerBase
{
    private readonly UrlShortenerService _urlShortenerService;

    public UrlController(UrlShortenerService urlShortenerService)
    {
        _urlShortenerService = urlShortenerService;
    }

    [HttpPost("shorten")]
    public async Task<IActionResult> ShortenUrl([FromBody] UrlRequestToDo request)
    {
        if (string.IsNullOrWhiteSpace(request.OriginalUrl))
            return BadRequest("URL не может быть пустым.");

        var userId = User.FindFirstValue(ClaimTypes.Name);
        var shortCode = await _urlShortenerService.ShortenUrlAsync(request.OriginalUrl, userId);
        return Ok(new { ShortUrl = $"https://short.ly/{shortCode}" });
    }

    [HttpGet("{shortCode}")]
    public async Task<IActionResult> RedirectUrl(string shortCode)
    {
        var originalUrl = await _urlShortenerService.GetOriginalUrlAsync(shortCode);
        if (originalUrl == null) return NotFound("URL не найден.");

        return Redirect(originalUrl);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllUrls()
    {
        var userId = User.FindFirstValue(ClaimTypes.Name);
        var isAdmin = User.IsInRole("Admin");
        var urls = await _urlShortenerService.GetAllUrlsAsync(userId, isAdmin);
        return Ok(urls);
    }

    [HttpDelete("delete/{shortCode}")]
    public async Task<IActionResult> Delete(string shortCode)
    {
        var userId = User.FindFirstValue(ClaimTypes.Name);
        var isAdmin = User.IsInRole("Admin");
        var result = await _urlShortenerService.DeleteUrlAsync(shortCode, userId, isAdmin);
        if (result)
        {
            return Ok();
        }
        return NotFound("URL не найден или у вас нет прав на его удаление.");
    }
}
