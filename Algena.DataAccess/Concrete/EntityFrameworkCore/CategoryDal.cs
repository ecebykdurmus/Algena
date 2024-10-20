using Algena.Core.DataAccess.EntityFrameworkCore;
using Algena.DataAccess.Abstract;
using Algena.DataAccess.Concrete.EntityFrameworkCore.Context;
using Algena.Entities.Concrete;

namespace Algena.DataAccess.Concrete.EntityFrameworkCore
{
    public class CategoryDal : RepositoryBase<Category>, ICategoryDal
    {
        public CategoryDal(AlgenaContext context):base(context)
        {
            
        }
    }
}
