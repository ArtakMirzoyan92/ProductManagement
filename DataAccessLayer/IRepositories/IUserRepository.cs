using DataAccessLayer.Entities;

namespace DataAccessLayer.IRepositories
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string userEmail);
        Task<User> AddUserAsync(User user);
    }
}
