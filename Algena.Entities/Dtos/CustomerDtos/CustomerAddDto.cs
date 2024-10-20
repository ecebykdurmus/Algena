using Algena.Core.Entities;
using Algena.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algena.Entities.Dtos.CustomerDtos
{
    public class CustomerAddDto : UserDto, IDto
    {
        public string Fax { get; set; }
    }
}
