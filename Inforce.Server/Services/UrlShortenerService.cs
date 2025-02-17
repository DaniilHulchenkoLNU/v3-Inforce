using Inforce.Server.DAL.Interfaces;
using Inforce.Server.Domain;

namespace Inforce.Server.Services;

public class UrlShortenerService
{
    private readonly IUrlRepository _urlRepository;

    public UrlShortenerService(IUrlRepository urlRepository)
    {
        _urlRepository = urlRepository;
    }

    public async Task<string> ShortenUrlAsync(string originalUrl, string createdBy)
    {
        var existingUrl = await _urlRepository.GetByOriginalUrlAsync(originalUrl, createdBy);
        if (existingUrl != null)
            return existingUrl.ShortCode;

        string shortCode = GenerateShortCode();
        while (await _urlRepository.GetByShortCodeAsync(shortCode) != null)
        {
            shortCode = GenerateShortCode();
        }

        var urlEntry = new ShortenedUrl
        {
            OriginalUrl = originalUrl,
            ShortCode = shortCode,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = createdBy
        };

        await _urlRepository.AddAsync(urlEntry);
        await _urlRepository.SaveChangesAsync();

        return shortCode;
    }

    public async Task<IEnumerable<ShortenedUrl>> GetAllUrlsAsync(string createdBy, bool isAdmin)
    {
        if (isAdmin)
        {
            return await _urlRepository.GetAllAsync();
        }
        return await _urlRepository.GetAllByUserAsync(createdBy);
    }

    public async Task<string?> GetOriginalUrlAsync(string shortCode)
    {
        var urlEntry = await _urlRepository.GetByShortCodeAsync(shortCode);
        return urlEntry?.OriginalUrl;
    }

    public async Task<bool> DeleteUrlAsync(string shortCode, string createdBy, bool isAdmin)
    {
        var urlEntry = await _urlRepository.GetByShortCodeAsync(shortCode);
        if (urlEntry != null && (urlEntry.CreatedBy == createdBy || isAdmin))
        {
            await _urlRepository.DeleteAsync(urlEntry);
            await _urlRepository.SaveChangesAsync();
            return true;
        }
        return false;
    }

    private string GenerateShortCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 6)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
