using Algena.Business.Abstract;
using Algena.DataAccess.Abstract;
using Algena.DataAccess.Concrete.EntityFrameworkCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algena.Business.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryDal CategoryDal {  get; }

        public ICustomerDal CustomerDal { get; }

        public IOrderDal OrderDal { get; }

        public IOrderDetailDal OrderDetailDal { get; }

        public IProductDal ProductDal { get; }

        public ISellerDal SellerDal { get; }


        AlgenaContext _context;

        //Dallarımızı tek bir yere topladık.

        public UnitOfWork(ICategoryDal categoryDal, ICustomerDal customerDal, IOrderDal orderDal, IOrderDetailDal orderDetailDal, IProductDal productDal, ISellerDal sellerDal, AlgenaContext context)
        {
            CategoryDal = categoryDal;
            CustomerDal = customerDal;
            OrderDal = orderDal;
            OrderDetailDal = orderDetailDal;
            ProductDal = productDal;
            SellerDal = sellerDal;
            _context = context;
        }

        //Ram'de kalmaması için;
        public void Dispose()
        {
            _context.Dispose();
        }

        //Save metodumuzu oluşturduk.
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
