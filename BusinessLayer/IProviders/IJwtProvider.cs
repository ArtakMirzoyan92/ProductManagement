using DataAccessLayer.Entities;

namespace BusinessLayer.IProviders
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}
