
using Algena.Core.DataAccess.EntityFrameworkCore;
using Algena.DataAccess.Abstract;
using Algena.DataAccess.Concrete.EntityFrameworkCore.Context;
using Algena.Entities.Concrete;

namespace Algena.DataAccess.Concrete.EntityFrameworkCore
{
    public class SellerDal : RepositoryBase<Seller>, ISellerDal
    {
        public SellerDal(AlgenaContext context):base(context)
        {
            
        }
    }
}
