using BusinessLayer.Models;

namespace BusinessLayer.IServices
{
    public interface IUsersService
    {
        Task<string> GetByEmailAsync(string userEmail, string password);
        Task AddUserAsync(CreateUserDto userDto);
    }
}
