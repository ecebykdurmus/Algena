using Algena.Core.Entities;
using Algena.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algena.Entities.Concrete
{
    public class Seller : MyUser, IEntity
    {
        public string CompanyName { get; set; }

        // public int AppUserId { get; set; }

        //Relational Properties
        public AppUser AppUser { get; set; }
        public ICollection<Order> Orders { get; set; }
        
    }
}
