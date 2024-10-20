using Algena.Core.Entities;
using Algena.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algena.Entities.Dtos.SellerDtos
{
    public class SellerAddDto : UserDto, IDto
    {
        public string CompanyName { get; set; }
    }
}
