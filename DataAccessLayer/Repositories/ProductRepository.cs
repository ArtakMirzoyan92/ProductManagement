using DataAccessLayer.DbContexts;
using DataAccessLayer.Entities;
using DataAccessLayer.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly TestDbContext _dbContext;
        public ProductRepository(TestDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Product>> AllProductAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product> AddAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(Guid productId)
        {
            Product product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<List<Product>> GetNameByFilterAsync(string name)
        {
            return await _dbContext.Products.Where(x => x.Name.Contains(name)).ToListAsync();
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            _dbContext?.Products.Update(product);
            return await _dbContext.SaveChangesAsync() > 0;
        }

    }
}
