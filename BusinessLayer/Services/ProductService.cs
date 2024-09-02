using AutoMapper;
using BusinessLayer.IServices;
using BusinessLayer.Models.Product;
using DataAccessLayer.Entities;
using DataAccessLayer.IRepositories;


namespace BusinessLayer.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _repository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductResponse>> GetAllAsync()
        {
            List<ProductResponse> listProductDto = null;
            List<Product> allProduct = await _repository.AllProductAsync();
            if (allProduct != null)
            {
                listProductDto = new List<ProductResponse>();

                foreach (var product in allProduct)
                {

                    listProductDto.Add(_mapper.Map<ProductResponse>(product));
                }
            }
            return listProductDto;
        }

        public async Task<List<ProductResponse>> GetNameByFilterAsync(string name)
        {
            List<ProductResponse> listProductDto = null;
            if (!string.IsNullOrWhiteSpace(name))
            {
                List<Product> allProduct = await _repository.GetNameByFilterAsync(name);
                if (allProduct != null)
                {
                    listProductDto = new List<ProductResponse>();
                    foreach (var product in allProduct)
                    {
                        listProductDto.Add(_mapper.Map<ProductResponse>(product));
                    }
                }
            }
            return listProductDto;
        }

        public async Task<ProductResponse> AddProductAsync(ProductRequest productDto)
        {
            Product product = _mapper.Map<Product>(productDto);
            Product productCreated = await _repository.AddAsync(product);
            return _mapper.Map<ProductResponse>(productCreated);
        }

        public async Task<bool> UpdateAsync(ProductUpdateRequest productDto)
        {
            Product product = _mapper.Map<Product>(productDto);

            return await _repository.UpdateAsync(product);
        }

        public async Task<bool> DeleteAsync(Guid productId)
        {
            return await _repository.DeleteAsync(productId);
        }
    }
}
