using Algena.Business.Abstract;
using Algena.Entities.Dtos.ProductDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Algena.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ProductDto productDto = await _productService.GetByIdAsync(id);
            return Ok(productDto);
        }

        [HttpGet]
        [Authorize(Roles = "customer")]
        public async Task<IActionResult> Get()
        {
            List<ProductDto> productDtos = await _productService.GetAllAsync();
            return productDtos is not null ? Ok(productDtos) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductAddDto productDto)
        {
            int response = await _productService.AddAsync(productDto);
            return response > 0 ? Ok("Ekleme işlemi BAŞARILI!") : NotFound("Ekleme işlemi BAŞARISIZ!!!");
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            int response = await _productService.UpdateAsync(productDto);
            return response > 0 ? Ok("Güncelleme işlemi BAŞARILI!") : NotFound("Güncelleme işlemi BAŞARISIZ!!!");
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            int response = await _productService.DeleteAsync(id);
            return response > 0 ? Ok("Silme işlemi BAŞARILI!") : NotFound("Silme işlemi BAŞARISIZ!!!");
        }
    }
}
