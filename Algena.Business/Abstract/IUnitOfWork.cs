using Algena.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algena.Business.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryDal CategoryDal { get; }
        ICustomerDal CustomerDal { get; }
        IOrderDal OrderDal { get; }
        IOrderDetailDal OrderDetailDal { get; }
        IProductDal ProductDal { get; }
        ISellerDal SellerDal { get; }
        Task<int> SaveAsync();

    }
}
