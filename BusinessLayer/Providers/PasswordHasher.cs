using BusinessLayer.IProviders;

namespace BusinessLayer.Providers
{
    public class PasswordHasher : IPasswordHasher
    {
        public string GenerateHash(string password) =>
           BCrypt.Net.BCrypt.EnhancedHashPassword(password);

        public bool VarifyPassword(string password, string passwordHash) =>
            BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
    }
}
