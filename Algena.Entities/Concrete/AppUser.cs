using Algena.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algena.Entities.Concrete
{
    public class AppUser : IdentityUser<int>, IEntity 
    {
        //Relational Properties
        public Seller Seller { get; set; }
        public Customer Customer { get; set; }
    }
}
