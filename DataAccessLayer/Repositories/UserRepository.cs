using DataAccessLayer.DbContexts;
using DataAccessLayer.Entities;
using DataAccessLayer.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TestDbContext _dbContext;

        public UserRepository(TestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetByEmailAsync(string userEmail)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == userEmail);
        }
    }
}
