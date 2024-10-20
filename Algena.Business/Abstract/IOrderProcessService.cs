using Algena.Entities.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algena.Business.Abstract
{
    public interface IOrderProcessService
    {
        Task<int> AddOrderAsync(OrderAddDto orderDto);
    }
}
