using Algena.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algena.Entities.Dtos.ProductDtos
{
    public class ProductAddDto : IDto
    {
        //public int SellerId { get; set; } //Bunu sonradan ekledik.
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int StockAmount { get; set; }
    }
}
