using Algena.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algena.Entities.Dtos.OrderDtos
{
    public class OrderAddDto : IDto
    {
        //public int CustomerId { get; set; }
        public int? SellerId { get; set; }
        public int ProductId { get; set; }
        //public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string UserName { get; set; }
    }
}
