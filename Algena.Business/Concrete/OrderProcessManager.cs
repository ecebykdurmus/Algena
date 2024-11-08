using Algena.Business.Abstract;
using Algena.DataAccess.Abstract;
using Algena.Entities.Concrete;
using Algena.Entities.Dtos.OrderDtos;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Algena.Business.Concrete
{
    public class OrderProcessManager : IOrderProcessService
    {
        //Sipariş işlemlerinin olduğu genel bir yer.

        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public OrderProcessManager(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }

        public async Task<int> AddOrderAsync(OrderAddDto orderDto)
        {
            int response;
            try
            {
                //Seller seller = _unitOfWork.SellerDal.GetAsync(x => x.Id == orderDto.SellerId).Result;
                Seller seller = orderDto.SellerId.HasValue ? await _unitOfWork.SellerDal.GetAsync(x => x.Id == orderDto.SellerId.Value) : null;
                AppUser user = _authService.GetUserAsync(orderDto.UserName).Result; //Sekron olsun diye Result koydum.
                Product product = _unitOfWork.ProductDal.GetAsync(x => x.Id == orderDto.ProductId).Result;

                Order order = new Order()
                {
                    CustomerId = user.Id,
                    //SellerId = seller?.Id
                };

                
                await _unitOfWork.OrderDal.AddAsync(order);
                await _unitOfWork.SaveAsync(); //2 kere SaveChanges yapmamızın sebebi order oluşmadan id'sini veremez ki...

                OrderDetail orderDetail = new OrderDetail()
                {
                    OrderId = order.Id,
                    Price = orderDto.Quantity * product.Price,
                    ProductId = product.Id,
                    Quantity = orderDto.Quantity
                };

                await _unitOfWork.OrderDetailDal.AddAsync(orderDetail);
                product.StockAmount -= orderDto.Quantity; //Satıldığı için quantitysi düşmeli.
                await _unitOfWork.ProductDal.UpdateAsync(product);
                response = await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                response = 0;
                //_unitOfWork.Dispose();
            }
            return response;
        }

        //public async Task<int> AddOrderAsync(int productId, int sellerId, int quantity, string userName)
        //{
        //    using (TransactionScope ts = new TransactionScope())
        //    {
        //        int response;
        //        try
        //        {
        //            Seller seller = _unitOfWork.SellerDal.GetAsync(x => x.Id == sellerId).Result;
        //            AppUser user = _unitOfWork.AuthService.GetUserAsync(userName).Result; //Sekron olsun diye Result koydum.
        //            Product product = _unitOfWork.ProductDal.GetAsync(x => x.Id == productId).Result;

        //            Order order = new Order()
        //            {
        //                CustomerId = user.Id,
        //                SellerId = seller.Id
        //            };

        //            await _unitOfWork.OrderDal.AddAsync(order);
        //            await _unitOfWork.SaveAsync(); //2 kere SaveChanges yapmamızın sebebi order oluşmadan id'sini veremez ki...

        //            OrderDetail orderDetail = new OrderDetail()
        //            {
        //                OrderId = order.Id,
        //                Price = quantity * product.Price,
        //                ProductId = product.Id,
        //                Quantity = quantity
        //            };

        //            await _unitOfWork.OrderDetailDal.AddAsync(orderDetail);
        //            product.StockAmount -= quantity; //Satıldığı için quantitysi düşmeli.
        //            await _unitOfWork.ProductDal.UpdateAsync(product);
        //            response = await _unitOfWork.SaveAsync();
        //            ts.Complete();
        //        }
        //        catch (Exception)
        //        {
        //            response = 0;
        //            ts.Dispose();
        //        }
        //        return response;
        //    }
        //}
    }
}
