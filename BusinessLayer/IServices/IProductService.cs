using BusinessLayer.Models.Product;

namespace BusinessLayer.IServices
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetAllAsync();
        Task<List<ProductResponse>> GetNameByFilterAsync(string name);
        Task<ProductResponse> AddProductAsync(ProductRequest productDto);
        Task<bool> UpdateAsync(ProductUpdateRequest productDto);
        Task<bool> DeleteAsync(Guid productId);
    }
}
