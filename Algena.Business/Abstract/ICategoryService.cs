using Algena.Entities.Dtos.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algena.Business.Abstract
{
    public interface ICategoryService
    {
        Task<CategoryDto> GetByIdAsync(int id);
        Task<List<CategoryDto>> GetByNameFilterAsync(string filter = "");
        Task<List<CategoryDto>> GetAllAsync();
        Task<int> DeleteAsync(int id);
        Task<int> AddAsync(CategoryAddDto categoryDto);
        Task<int> UpdateAsync(CategoryDto categoryDto);    
    }
}
