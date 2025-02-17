using Inforce.Server.Domain;

namespace Inforce.Server.DAL.Interfaces;

public interface IUrlRepository
{
    Task<IEnumerable<ShortenedUrl>> GetAllByUserAsync(string createdBy);
    Task<IEnumerable<ShortenedUrl>> GetAllAsync();
    Task<ShortenedUrl?> GetByShortCodeAsync(string shortCode);
    Task<ShortenedUrl?> GetByOriginalUrlAsync(string originalUrl, string createdBy);
    Task AddAsync(ShortenedUrl url);
    Task DeleteAsync(ShortenedUrl url);
    Task SaveChangesAsync();
}

