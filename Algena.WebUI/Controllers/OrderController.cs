using Algena.Business.Abstract;
using Algena.Entities.Dtos.OrderDtos;
using Algena.WebUI.BasketTransaction;
using Algena.WebUI.BasketTransaction.BasketModels;
using Microsoft.AspNetCore.Mvc;

namespace Algena.WebUI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderProcessService _orderProcessService;
        private readonly IBasketTransaction _basketTransaction;
        public OrderController(IOrderProcessService orderProcessService, IBasketTransaction basketTransaction)
        {
            _orderProcessService = orderProcessService;
            _basketTransaction = basketTransaction;
        }

        public async Task<IActionResult> Index(List<OrderAddDto> orderAddDtos)
        {
            Basket basket = _basketTransaction.GetOrCreateBasket();
            int response = 0;
            //basket.BasketItems.ForEach(async item =>
            //{
            //    response = await _orderProcessService.AddOrderAsync(new OrderAddDto()
            //    {
            //        //SellerId = item.SellerId != 0 ? item.SellerId : (int?)null,
            //        SellerId = (int?)null,
            //        ProductId = item.ProductId,
            //        Quantity = item.Quantity,
            //        UserName = User.Identity.Name
            //    });
            //});

            foreach (var item in basket.BasketItems)
            {
                response = await _orderProcessService.AddOrderAsync(new OrderAddDto()
                {
                    SellerId = item.SellerId != 0 ? item.SellerId : (int?)null,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UserName = User.Identity.Name
                });
            }


            if (response > 0)
            {
                _basketTransaction.DeleteBasket();
            }

            return response > 0 ? RedirectToAction("Index", "Basket", new { message = "Siparişiniz oluşturuldu." }) : RedirectToAction("Index", "Basket", new { message = "Siparişiniz oluşturulurken bir sorun ile karşılaştık. Lütfen daha sonra tekrar deneyiniz." });
        }


    }
}
