using Algena.Business.Abstract;
using Algena.DataAccess.Abstract;
using Algena.Entities.Concrete;
using Algena.Entities.Dtos.ProductDtos;


namespace Algena.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductManager(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddAsync(ProductAddDto productDto)
        {
            Product urun = _unitOfWork.ProductDal.GetAsync(x => x.ProductName == productDto.ProductName).Result;

            if (urun is null)
            {
                Product product = new Product()
                {
                    CategoryId = productDto.CategoryId,
                    Price = productDto.Price,
                    ProductName = productDto.ProductName,
                    StockAmount = productDto.StockAmount,

                };
                await _unitOfWork.ProductDal.AddAsync(product);
                var a = await _unitOfWork.SaveAsync();
            }
            else
            {
                urun.StockAmount += productDto.StockAmount; //Ürünün StockAmount'unu arttırdık.
                await _unitOfWork.ProductDal.UpdateAsync(urun); // ve güncelledik.
            }
            var b = await _unitOfWork.SaveAsync();
            return b;
          }

        public async Task<int> DeleteAsync(int id)
        {
            Product product = await _unitOfWork.ProductDal.GetAsync(x => x.Id == id);
            await _unitOfWork.ProductDal.DeleteAsync(product);
            return await _unitOfWork.SaveAsync();
        }

        public async Task<List<ProductDto>> GetAllByNameFilterAsync(string filter = "")
        {
            List<Product> products = await _unitOfWork.ProductDal.GetAllAsync(x => x.ProductName.Contains(filter));
            
            List<ProductDto> productDtos = new List<ProductDto>();

            foreach (Product product in products) 
            {
                productDtos.Add(new ProductDto()
                {
                    ProductName = product.ProductName,
                    CategoryId = product.CategoryId,
                    Price = product.Price,
                    Id = product.Id,
                    StockAmount = product.StockAmount
                });
            }
            return productDtos;
        }


        public async Task<List<ProductDto>> GetAllAsync()
        {
            List<Product> products = await _unitOfWork.ProductDal.GetAllAsync();

            List<ProductDto> productDtos = new List<ProductDto>();

            foreach (Product product in products)
            {
                productDtos.Add(new ProductDto()
                {
                    ProductName = product.ProductName,
                    CategoryId = product.CategoryId,
                    Price = product.Price,
                    Id = product.Id,
                    StockAmount = product.StockAmount
                });
            }
            return productDtos;
        }


        public async Task<List<ProductDto>> GetAllOrderByDesPriceAsync()
        {
            return (await GetAllByNameFilterAsync()).OrderByDescending(x => x.Price).ToList();
        }

        public async Task<List<ProductDto>> GetAllOrderByPriceAsync()
        {
            return (await GetAllByNameFilterAsync()).OrderBy(x => x.Price).ToList();
        }

        public async Task<List<ProductDto>> GetAllSellerByIdAsync(int id)
        {
            return null;
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            Product product = await _unitOfWork.ProductDal.GetAsync(x => x.Id == id);
            return new ProductDto()
            {
                ProductName = product.ProductName,
                CategoryId = product.CategoryId,
                Price = product.Price,
                Id = product.Id,
                StockAmount = product.StockAmount
            };
        }

        public async Task<int> UpdateAsync(ProductDto productDto)
        {
            Product product = await _unitOfWork.ProductDal.GetAsync(x => x.Id == productDto.Id);

            product.Price = productDto.Price;
            product.StockAmount = productDto.StockAmount;
            product.ProductName = productDto.ProductName;
            product.CategoryId = productDto.CategoryId;

            await _unitOfWork.ProductDal.UpdateAsync(product);
            return await _unitOfWork.SaveAsync();
        }
    }
}
