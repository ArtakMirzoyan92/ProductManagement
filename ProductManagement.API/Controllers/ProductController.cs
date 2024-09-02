using BusinessLayer.IServices;
using BusinessLayer.Models.Product;
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
        public async Task<IActionResult> GetAllProduct()
        {
            IList<ProductResponse> response = await _productService.GetAllAsync();

            return Ok(response);
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetProductsByFilter(string name)
        {
            var response = await _productService.GetNameByFilterAsync(name);

            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(ProductRequest productDto)
        {
            ProductResponse response = await _productService.AddProductAsync(productDto);

            return Ok(response);
        }

        //[Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(ProductUpdateRequest productDto)
        {
            var isUpdated = await _productService.UpdateAsync(productDto);
            if (isUpdated)
            {
                return Ok(isUpdated);
            }
            return BadRequest(isUpdated);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid productId)
        {
            var isDeleted = await _productService.DeleteAsync(productId);
            if (isDeleted)
            {
                return Ok(isDeleted);
            }
            return BadRequest(isDeleted);
        }
    }
}
