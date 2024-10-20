using Algena.Business.Abstract;
using Algena.DataAccess.Abstract;
using Algena.DataAccess.Concrete.EntityFrameworkCore;
using Algena.Entities.Concrete;
using Algena.Entities.Dtos.CategoryDtos;
using Algena.Entities.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algena.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            List<Category> categories = await _unitOfWork.CategoryDal.GetAllAsync();
            List<CategoryDto> categoryDtos = new List<CategoryDto>();
            foreach (Category category in categories)
            {
                CategoryDto categoryDto = new CategoryDto() 
                {
                    CategoryName = category.CategoryName,
                    Description = category.Description,
                    Id = category.Id, //Id koyulmamıştı.
                };
                categoryDtos.Add(categoryDto);
            }
            return categoryDtos;
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            Category category = await _unitOfWork.CategoryDal.GetAsync(x => x.Id == id);
            CategoryDto categoryDto = new CategoryDto() 
            { 
                Description = category.Description,
                CategoryName = category.CategoryName,
                Id = category.Id
            };
            return categoryDto;
            
        }

        public async Task<List<CategoryDto>> GetByNameFilterAsync(string filter = "")
        {
            
            List<Category> categories = await _unitOfWork.CategoryDal.GetAllAsync(x => x.CategoryName.Contains(filter));

            List<CategoryDto> categoryDtos = new List<CategoryDto>();

            foreach (Category category in categories)
            {
                categoryDtos.Add(new CategoryDto()
                {
                     CategoryName = category.CategoryName, 
                     Description = category.Description,
                     Id = category.Id

                });
            }
            return categoryDtos;
        }

        public async Task<int> AddAsync(CategoryAddDto categoryDto)
        {
            Category cat = await _unitOfWork.CategoryDal.GetAsync(x => x.CategoryName == categoryDto.CategoryName);
            
            if(cat is not null)
            {
                return -1;
            }

            Category category = new Category()
            {
                CategoryName = categoryDto.CategoryName,
                Description = categoryDto.Description    
            };

            await _unitOfWork.CategoryDal.AddAsync(category);
            return await _unitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            Category category = await _unitOfWork.CategoryDal.GetAsync(x => x.Id == id);
            await _unitOfWork.CategoryDal.DeleteAsync(category);
            return await _unitOfWork.SaveAsync();
        }

        public async Task<int> UpdateAsync(CategoryDto categoryDto)
        {
            Category category = await _unitOfWork.CategoryDal.GetAsync(x => x.Id == categoryDto.Id);
            category.Description = categoryDto.Description;
            category.CategoryName = categoryDto.CategoryName;
            
            await _unitOfWork.CategoryDal.UpdateAsync(category);
            return await _unitOfWork.SaveAsync();
        }
    }
}
