using Algena.Business.Abstract;
using Algena.DataAccess.Concrete.EntityFrameworkCore;
using Algena.Entities.Dtos.CategoryDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Algena.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            CategoryDto categoryDto = await _categoryService.GetByIdAsync(id);
            return categoryDto is not null ? Ok(categoryDto) : BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<CategoryDto> categoryDtos = await _categoryService.GetAllAsync();
            return categoryDtos is not null ? Ok(categoryDtos) : BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddDto categoryDto)
        {
            int response = await _categoryService.AddAsync(categoryDto);
            return response > 0 ? Ok("Ekleme işlemi BAŞARILI!") : BadRequest("Ekleme işlemi BAŞARISIZ!!!");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            int response = await _categoryService.DeleteAsync(id);
            return response > 0 ? Ok("Silme işlemi BAŞARILI!") : BadRequest("Silme işlemi BAŞARISIZ!!!");
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryDto categoryDto)
        {
            int response = await _categoryService.UpdateAsync(categoryDto);
            return response > 0 ? Ok("Güncelleme işlemi BAŞARILI!") : BadRequest("Güncelleme işlemi BAŞARISIZ!!!");
        }


    }
}
