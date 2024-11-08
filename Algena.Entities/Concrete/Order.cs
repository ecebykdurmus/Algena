using Algena.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algena.Entities.Concrete
{
    public class Order : IEntity
    {
        public Order()
        {
            OrderDate = DateTime.Now;
        }
        public int Id { get; set; }
        public int CustomerId { get; set; }
        //public int? SellerId { get; set; }
        //public System.Nullable<int> SellerId { get; set; }
        public DateTime OrderDate { get; set; }

        //Relational Properties
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public Customer Customer { get; set; }

    }
}
