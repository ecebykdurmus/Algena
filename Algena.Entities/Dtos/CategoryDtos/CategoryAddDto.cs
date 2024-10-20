using Algena.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algena.Entities.Dtos.CategoryDtos
{
    public class CategoryAddDto : IDto
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
