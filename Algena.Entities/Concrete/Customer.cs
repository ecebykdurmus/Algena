using Algena.Core.Entities;
using Algena.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algena.Entities.Concrete
{
    public class Customer : MyUser, IEntity
    {
        
        public string Fax { get; set; }

        //public int AppUserId;

        //Relational Properties
        public AppUser AppUser { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}
