using Inforce.Server.Domain;

namespace Inforce.Server.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task AddAsync(User user);
        Task<bool> UserExistsAsync(string username);
        Task SaveChangesAsync();
    }

}
