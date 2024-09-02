using BusinessLayer.Models.Auth;

namespace BusinessLayer.IServices
{
    public interface IUsersService
    {
        Task<string> GetByEmailAsync(string userEmail, string password);
        Task<UserResponse> AddUserAsync(UserRegisterRequest userDto);
    }
}
