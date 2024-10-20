using Algena.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algena.Entities.Concrete
{
    public class OrderDetail : IEntity
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        int quantity = 0;
        public int Quantity 
        {
            get
            {
                return quantity;
            }
            set
            { 
                if(value < 1)
                {
                    quantity = 1;
                }
                else
                {
                    quantity = value;
                }
            }
        
        }

        //Realtional Properties
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
