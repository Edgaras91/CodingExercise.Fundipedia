using Microsoft.AspNetCore.Mvc;
using RepairShop.Api.Models.Requests;
using RepairShop.Application.Services.Interfaces;

namespace RepairShop.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderStatusService _orderStatusService;

        public OrdersController(IOrderStatusService orderStatusService)
        {
            _orderStatusService = orderStatusService;
        }

        [HttpGet("status")]
        public async Task<string> Get([FromQuery] OrderStatusRequest request)
        {
            var orderStatus = await _orderStatusService.GetOrderStatusAsync(request.IsRushOrder, request.OrderType,
                request.IsNewCustomer, request.IsLargeOrder);
            return orderStatus.Status;
        }
    }
}