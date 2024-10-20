using Algena.Entities.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algena.Business.Abstract
{
    public interface IProductService
    {
        Task<ProductDto> GetByIdAsync(int id);
        Task<List<ProductDto>> GetAllAsync();

        Task<List<ProductDto>> GetAllByNameFilterAsync(string filter = "");
        Task<List<ProductDto>> GetAllOrderByPriceAsync();
        Task<List<ProductDto>> GetAllOrderByDesPriceAsync();
        Task<List<ProductDto>> GetAllSellerByIdAsync(int id);
        Task<int> AddAsync(ProductAddDto productDto);
        Task<int> UpdateAsync(ProductDto productDto);
        Task<int> DeleteAsync(int id);
    }
}
