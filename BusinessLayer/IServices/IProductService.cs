using BusinessLayer.Models;

namespace BusinessLayer.IServices
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllAsync();
        Task<List<ProductDto>> GetNameByFilterAsync(string name);
        Task<ProductDto> AddProductAsync(ProductDto productDto);
        Task<bool> UpdateAsync(ProductDto productDto);
        Task<bool> DeleteAsync(Guid productId);
    }
}
