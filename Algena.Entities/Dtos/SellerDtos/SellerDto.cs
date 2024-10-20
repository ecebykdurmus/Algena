using Algena.Core.Entities;
using Algena.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algena.Entities.Dtos.SellerDtos
{
    public class SellerDto : UserDto, IDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
    }
}
