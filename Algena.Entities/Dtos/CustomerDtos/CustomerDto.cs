using Algena.Core.Entities;
using Algena.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algena.Entities.Dtos.CustomerDtos
{
    public class CustomerDto : UserDto, IDto
    {
        public int Id { get; set; }
        public string Fax { get; set; }
    }
}
