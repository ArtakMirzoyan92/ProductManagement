using BusinessLayer.IServices;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProductManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<List<ProductDto>> GetAllProduct()
        {
            var allProduct = await _productService.GetAllAsync();
            return allProduct;
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetProductsByFilter(string name)
        {
            var product = await _productService.GetNameByFilterAsync(name);
            return Ok(product);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(ProductDto productDto)
        {
            var product = await _productService.AddProductAsync(productDto);
            return Ok(product);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(ProductDto productDto)
        {
            var isUpdated = await _productService.UpdateAsync(productDto);
            if (isUpdated)
            {
                return Ok();
            }
            return BadRequest();
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid productId)
        {
            var isDeleted = await _productService.DeleteAsync(productId);
            if (isDeleted)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
