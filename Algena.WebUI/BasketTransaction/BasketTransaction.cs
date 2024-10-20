using Algena.WebUI.BasketTransaction.BasketModels;
using Newtonsoft.Json;

namespace Algena.WebUI.BasketTransaction
{
    public class BasketTransaction : IBasketTransaction
    {
        private const string basketName = "basket";
        private string serializeItem;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BasketTransaction(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void DeleteItem(int productId)
        {
            Basket basket = GetOrCreateBasket();
            BasketItem basketItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == productId);
            basket.BasketItems.Remove(basketItem);
            serializeItem = JsonConvert.SerializeObject(basket);
            _httpContextAccessor.HttpContext.Response.Cookies.Append(basketName, serializeItem);
        }

        public Basket GetOrCreateBasket()
        {
            bool cookieCheck = _httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(basketName);
            string cookie = _httpContextAccessor.HttpContext.Request.Cookies[basketName];
            return cookieCheck ? JsonConvert.DeserializeObject<Basket>(cookie) : new Basket() { BasketItems = new List<BasketItem>() };
        }

        public void AddOrUpdateItem(BasketItem basketItem)
        {
            Basket basket = GetOrCreateBasket();
            BasketItem _basketItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == basketItem.ProductId);
            if (_basketItem is not null) _basketItem.Quantity += 1;
            else basket.BasketItems.Add(basketItem);

            serializeItem = JsonConvert.SerializeObject(basket);
            _httpContextAccessor.HttpContext.Response.Cookies.Append(basketName, serializeItem);

        }

        public void Decrease(int productId)
        {
            Basket basket = GetOrCreateBasket();
            BasketItem basketItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == productId);
            if (basketItem.Quantity > 1) basketItem.Quantity -= 1;
            else basket.BasketItems.Remove(basketItem);
            //Nesneyi serialize etmek.
            serializeItem = JsonConvert.SerializeObject(basket);
            //Cookie uygulama.
            _httpContextAccessor.HttpContext.Response.Cookies.Append(basketName, serializeItem);

        }

        public void Increase(int productId)
        {
            Basket basket = GetOrCreateBasket();
            BasketItem basketItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == productId);
            basketItem.Quantity += 1;

                
            //Nesneyi serialize etmek.
            serializeItem = JsonConvert.SerializeObject(basket);
            //Cookie uygulama.
            _httpContextAccessor.HttpContext.Response.Cookies.Append(basketName, serializeItem);
        }

        public void DeleteBasket()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(basketName);
        }
    }
}
