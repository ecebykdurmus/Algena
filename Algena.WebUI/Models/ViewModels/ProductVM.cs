namespace Algena.WebUI.Models.ViewModels
{
    public class ProductVM
    {
        //ProductDto'nun buradaki hali gibi düşünebiliriz.
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int StockAmount { get; set; }
    }
}
