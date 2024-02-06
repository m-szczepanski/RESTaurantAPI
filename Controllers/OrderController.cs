﻿using System.Collections.Generic;
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
        private readonly OrderService orderService;

        public OrderController(OrderService orederService, IMapper _mapper)
        {
            this._mapper = _mapper;
            this.orderService = orederService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<OrderDto>>> GetAll(
            CancellationToken cancellationToken, int? skip = null, int? limit = null)
        {
            var orders = await this.orderService.GetAll(cancellationToken);
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
            var order = await orderService.GetById(id, cancellationToken);
            var orderDto = this._mapper.Map<TableDto>(order);
            if (orderDto == null)
            {
                return NotFound();
            }

            return Ok(orderDto);
        }

        [HttpPost("AddOrder")]
        public async Task<ActionResult<OrderDto>> AddOrder(int quantity, int tableId, int dishId, CancellationToken cancellationToken)
        {
            var order = await this.orderService.AddOrder(quantity, tableId, dishId, cancellationToken);
            var orderDto = this._mapper.Map<OrderDto>(order);

            return Ok(orderDto);
        }

        [HttpPut("MarkAsDelivered/{id}")]
        public async Task<ActionResult> MarkAsDelivered(int id, CancellationToken cancellationToken)
        {
            await orderService.MarkAsDelivered(id, cancellationToken);

            return Ok("The order has been delivered.");
        }

        [HttpDelete("Delete{id}")]
        public async Task<ActionResult> DeleteOrder(int id, CancellationToken cancellationToken)
        {
            await this.orderService.DeleteOrder(id, cancellationToken);

            return Ok("Order has been deleted.");
        }
    }
}
