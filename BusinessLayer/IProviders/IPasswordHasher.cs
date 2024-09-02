namespace BusinessLayer.IProviders
{
    public interface IPasswordHasher
    {
        string GenerateHash(string password);
        bool VarifyPassword(string password, string passwordHash);
    }
}
