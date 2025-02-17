using Inforce.Server.DAL.Interfaces;
using Inforce.Server.Domain;
using Microsoft.EntityFrameworkCore;

namespace Inforce.Server.DAL.Implementations;

public class UrlRepository : IUrlRepository
{
    private readonly ApplicationDbContext _context;

    public UrlRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ShortenedUrl>> GetAllByUserAsync(string createdBy)
    {
        return await _context.Urls  
            .Where(url => url.CreatedBy == createdBy)
            .ToListAsync();
    }

    public async Task<IEnumerable<ShortenedUrl>> GetAllAsync()
    {
        return await _context.Urls.ToListAsync();
    }

    public async Task<ShortenedUrl?> GetByShortCodeAsync(string shortCode)
    {
        return await _context.Urls
            .FirstOrDefaultAsync(url => url.ShortCode == shortCode);
    }

    public async Task<ShortenedUrl?> GetByOriginalUrlAsync(string originalUrl, string createdBy)
    {
        return await _context.Urls
            .FirstOrDefaultAsync(url => url.OriginalUrl == originalUrl && url.CreatedBy == createdBy);
    }

    public async Task AddAsync(ShortenedUrl url)
    {
        await _context.Urls.AddAsync(url);
    }

    public async Task DeleteAsync(ShortenedUrl url)
    {
        _context.Urls.Remove(url);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
