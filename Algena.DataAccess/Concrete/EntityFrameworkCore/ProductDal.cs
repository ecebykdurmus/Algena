using Algena.Core.DataAccess.EntityFrameworkCore;
using Algena.DataAccess.Abstract;
using Algena.DataAccess.Concrete.EntityFrameworkCore.Context;
using Algena.Entities.Concrete;

namespace Algena.DataAccess.Concrete.EntityFrameworkCore
{
    public class ProductDal : RepositoryBase<Product>, IProductDal
    {
        public ProductDal(AlgenaContext context) : base(context)
        {
            
        }
    }
}
