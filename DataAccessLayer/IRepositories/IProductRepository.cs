using DataAccessLayer.Entities;

namespace DataAccessLayer.IRepositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetNameByFilterAsync(string name);
        Task<Product> AddAsync(Product product);
        Task<List<Product>> AllProductAsync();
        Task<bool> UpdateAsync(Product product);
        Task<bool> DeleteAsync(Guid productId);
    }
}
