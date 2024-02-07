using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RESTaurantAPI.DTOs;
using RESTaurantAPI.Services;

namespace RESTaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class OrderController : Controller
    {
        private readonly IMapper _mapper;
        private readonly OrderService _orderService;

        public OrderController(OrderService orederService, IMapper _mapper)
        {
            this._mapper = _mapper;
            this._orderService = orederService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<OrderDto>>> GetAll(
            CancellationToken cancellationToken, int? skip = null, int? limit = null)
        {
            var orders = await this._orderService.GetAll(cancellationToken);
            var ordersDto = this._mapper.Map<List<OrderDto>>(orders);

            if (skip.HasValue)
            {
                ordersDto = ordersDto.Skip(skip.Value).ToList();
            }

            if (limit.HasValue)
            {
                ordersDto = ordersDto.Take(limit.Value).ToList();
            }

            return Ok(ordersDto);
        }


        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<OrderDto>> GetById(int id, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetById(id, cancellationToken);
            var orderDto = this._mapper.Map<OrderDto>(order);
            if (orderDto == null)
            {
                return NotFound();
            }

            return Ok(orderDto);
        }

        [HttpGet("GetInProgressOrders")]
        public async Task<ActionResult<List<OrderDto>>> GetInProgressOrders(
            CancellationToken cancellationToken, int? skip = null, int? limit = null)
        {
            var orders = await this._orderService.GetInPreparationOrders(cancellationToken);
            var ordersDto = this._mapper.Map<List<OrderDto>>(orders);

            if (skip.HasValue)
            {
                ordersDto = ordersDto.Skip(skip.Value).ToList();
            }

            if (limit.HasValue)
            {
                ordersDto = ordersDto.Take(limit.Value).ToList();
            }

            return Ok(ordersDto);
        }

        [HttpGet("GetDeliveredOrders")]
        public async Task<ActionResult<List<OrderDto>>> GetDeliveredOrders(
            CancellationToken cancellationToken, int? skip = null, int? limit = null)
        {
            var orders = await this._orderService.GetDeliveredOrders(cancellationToken);
            var ordersDto = this._mapper.Map<List<OrderDto>>(orders);

            if (skip.HasValue)
            {
                ordersDto = ordersDto.Skip(skip.Value).ToList();
            }

            if (limit.HasValue)
            {
                ordersDto = ordersDto.Take(limit.Value).ToList();
            }

            return Ok(ordersDto);
        }

        [HttpGet("GetTablesOrders/{tableId}")]
        public async Task<ActionResult<List<OrderDto>>> GetTablesOrders(
            int tableId, CancellationToken cancellationToken, int? skip = null, int? limit = null)
        {
            var orders = await this._orderService.GetAllTablesOrders(tableId, cancellationToken);
            var ordersDto = this._mapper.Map<List<OrderDto>>(orders);

            if (skip.HasValue)
            {
                ordersDto = ordersDto.Skip(skip.Value).ToList();
            }

            if (limit.HasValue)
            {
                ordersDto = ordersDto.Take(limit.Value).ToList();
            }

            return Ok(ordersDto);
        }

        [HttpPost("AddOrder")]
        public async Task<ActionResult<OrderDto>> AddOrder(int quantity, int tableId, int dishId, CancellationToken cancellationToken)
        {
            var order = await this._orderService.AddOrder(quantity, tableId, dishId, cancellationToken);
            var orderDto = this._mapper.Map<OrderDto>(order);

            return Ok(orderDto);
        }

        [HttpPut("MarkAsDelivered/{id}")]
        public async Task<ActionResult> MarkAsDelivered(int id, CancellationToken cancellationToken)
        {
            await _orderService.MarkAsDelivered(id, cancellationToken);

            return Ok("The order has been delivered.");
        }

        [HttpDelete("Delete{id}")]
        public async Task<ActionResult> DeleteOrder(int id, CancellationToken cancellationToken)
        {
            await this._orderService.DeleteOrder(id, cancellationToken);

            return Ok("Order has been deleted.");
        }
    }
}
