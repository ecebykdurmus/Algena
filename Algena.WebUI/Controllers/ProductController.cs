using Algena.Business.Abstract;
using Algena.Entities.Dtos.CategoryDtos;
using Algena.Entities.Dtos.ProductDtos;
using Algena.WebUI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Algena.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        List<ProductVM> productVMs = new List<ProductVM>();

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
            Doldur();
        }

        public async Task<IActionResult> Index()
        {
            return View(productVMs);
        }

        public async Task Doldur()
        {
            List<ProductDto> productDtos = await _productService.GetAllByNameFilterAsync();

            List<CategoryDto> categoryDtos = _categoryService.GetAllAsync().Result; 

            foreach (ProductDto productDto in productDtos)
            {
                ProductVM productVM = new ProductVM()
                {
                    ProductName = productDto.ProductName,
                    CategoryName = categoryDtos.FirstOrDefault(x => x.Id == productDto.CategoryId).CategoryName,
                    CategoryId = categoryDtos.FirstOrDefault(x => x.Id == productDto.CategoryId).Id,
                    Price = productDto.Price,
                    StockAmount = productDto.StockAmount,
                    Id = productDto.Id
                };
                productVMs.Add(productVM);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            await Doldur();
            ProductVM productVM = productVMs.FirstOrDefault(x => x.Id == id);
            if (productVM == null)
            {
                return NotFound();  // Ürün bulunamadıysa 404 sayfasına yönlendirin.
            }
            return View(productVM);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            List<CategoryDto> categoryDtos = await _categoryService.GetAllAsync();
            return View(categoryDtos);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductAddDto productAddDto)
        {
            int response = await _productService.AddAsync(productAddDto);
            return response > 0 ? RedirectToAction("Index") : View(productAddDto);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            //ProductDto productDto = await _productService.GetByIdAsync(id);
            //return View(productDto);
            await Doldur();
            ProductVM productVM = productVMs.FirstOrDefault(x => x.Id == id);
            if (productVM == null)
            {
                return NotFound();
            }
            ViewBag.Categories = _categoryService.GetAllAsync().Result;
            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            int response = await _productService.UpdateAsync(productDto);
            return response > 0 ? RedirectToAction("Index") : View(productDto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            int res = await _productService.DeleteAsync(id);
            return res > 0 ? RedirectToAction("Index") : RedirectToAction("Details", new { id = id}); 
        }
    }
}
