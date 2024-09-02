using AutoMapper;
using BusinessLayer.IServices;
using BusinessLayer.Models;
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

        public async Task<List<ProductDto>> GetAllAsync()
        {
            List<ProductDto> listProductDto = null;
            List<Product> allProduct = await _repository.AllProductAsync();
            if (allProduct != null)
            {
                listProductDto = new List<ProductDto>();

                foreach (var product in allProduct)
                {

                    listProductDto.Add(_mapper.Map<ProductDto>(product));
                }
            }
            return listProductDto;
        }

        public async Task<ProductDto> AddProductAsync(ProductDto productDto)
        {
            Product product = _mapper.Map<Product>(productDto);
            Product productCreated = await _repository.AddAsync(product);
            return _mapper.Map<ProductDto>(productCreated);
        }

        public async Task<bool> DeleteAsync(Guid productId)
        {
            return await _repository.DeleteAsync(productId);
        }

        public async Task<List<ProductDto>> GetNameByFilterAsync(string name)
        {
            List<ProductDto> listProductDto = null;
            if (!string.IsNullOrWhiteSpace(name))
            {
                List<Product> allProduct = await _repository.GetNameByFilterAsync(name);
                if (allProduct != null)
                {
                    listProductDto = new List<ProductDto>();
                    foreach (var product in allProduct)
                    {
                        listProductDto.Add(_mapper.Map<ProductDto>(product));
                    }
                }
            }
            return listProductDto;
        }

        public async Task<bool> UpdateAsync(ProductDto productDto)
        {
            Product product = _mapper.Map<Product>(productDto);
            return await _repository.UpdateAsync(product);
        }
    }
}
