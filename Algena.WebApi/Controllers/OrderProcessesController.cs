using Algena.Business.Abstract;
using Algena.Entities.Dtos.OrderDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Algena.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProcessesController : ControllerBase
    {
        private readonly IOrderProcessService _orderProcessService;

        public OrderProcessesController(IOrderProcessService orderProcessService)
        {
            _orderProcessService = orderProcessService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderAddDto orderDto)
        {
            int response = await _orderProcessService.AddOrderAsync(orderDto);
            return Ok(response);

        }
    }
}
