using Algena.Core.DataAccess.EntityFrameworkCore;
using Algena.DataAccess.Abstract;
using Algena.DataAccess.Concrete.EntityFrameworkCore.Context;
using Algena.Entities.Concrete;

namespace Algena.DataAccess.Concrete.EntityFrameworkCore
{
    public class OrderDetailDal : RepositoryBase<OrderDetail>, IOrderDetailDal
    {
        public OrderDetailDal(AlgenaContext context) : base(context)
        {
            
        }
    }
}
