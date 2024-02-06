using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RESTaurantAPI.DTOs;
using RESTaurantAPI.Models;
using RESTaurantAPI.Services;

namespace RESTaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class OrderController : Controller
    {
        private readonly IMapper _mapper;
        private readonly OrderService orderService;

        public OrderController(OrderService orederService, IMapper _mapper)
        {
            this._mapper = _mapper;
            this.orderService = orederService;
        }

        [HttpPost("AddOrder")]
        public async Task<ActionResult<OrderDto>> AddOrder(DateTime orderTime, int quantity, string status, int tableId, int dishId, CancellationToken cancellationToken)
        {
            var order = await this.orderService.AddOrder(orderTime, quantity, status, tableId, dishId, cancellationToken);
            var orderDto = this._mapper.Map<OrderDto>(order);

            return Ok(orderDto);
        }
    }
}
