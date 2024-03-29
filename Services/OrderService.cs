﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RESTaurantAPI.Data;
using Microsoft.EntityFrameworkCore;
using RESTaurantAPI.Models;
using RESTaurantAPI.HelpingServices;


namespace RESTaurantAPI.Services
{
    public class OrderService
    {
        private readonly APIDbContext _dbContext;

        public OrderService(APIDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<List<Order>> GetAll(CancellationToken cancellationToken, int? skip = null, int? limit = null)
        {
            var orders = await _dbContext.Orders
                .Include(x=>x.Dish)
                .Include(x=>x.Table)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return orders == null ? throw new ApplicationException("No orders are in the database right now.") : orders;
        }

        public async Task<Order> GetById(int id, CancellationToken cancellationToken)
        {
            var order = await this._dbContext.Orders.Where(x => x.Id == id)
                .Include(x => x.Dish)
                .Include(x => x.Table)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            return order == null ? throw new ApplicationException("No order found") : order;
        }

        public async Task<List<Order>> GetInPreparationOrders(CancellationToken cancellationToken, int? skip = null, int? limit = null)
        {
            var orders = await this._dbContext.Orders.Where(x=>x.Status == "in preparation")
                .Include(x => x.Dish)
                .Include(x => x.Table)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return orders == null ? throw new ApplicationException("There are no orders that are in progress.") : orders;
        }

        public async Task<List<Order>> GetDeliveredOrders(CancellationToken cancellationToken, int? skip = null, int? limit = null)
        {
            var orders = await this._dbContext.Orders.Where(x => x.Status == "delivered")
                .Include(x => x.Dish)
                .Include(x => x.Table)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return orders == null ? throw new ApplicationException("There are no orders that are in progress.") : orders;
        }

        public async Task<List<Order>> GetAllTablesOrders(int tableId, CancellationToken cancellationToken, int? skip = null, int? limit = null)
        {
            var orders = await this._dbContext.Orders.Where(x => x.Table.Id == tableId)
                .Include(x => x.Dish)
                .Include(x => x.Table)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return orders == null ? throw new ApplicationException("There are no orders that are in progress.") : orders;
        }

        public async Task<Order> AddOrder(int quantity, int tableId, int dishId, CancellationToken cancellationToken)
        {

            Table table = await this._dbContext.Tables.GetTableById(tableId, cancellationToken);
            Dish dish = await this._dbContext.Dishes.GetDishById(dishId, cancellationToken);

            var orderPrice = dish.Price * quantity;
            await TableHelpers.GetTableById(_dbContext.Tables,tableId, cancellationToken);
            await DishHelpers.GetDishById(_dbContext.Dishes, dishId, cancellationToken);

            var newOrder = new Order
            {
                OrderTime = DateTime.Now,
                Quantity = quantity,
                Status = "in preparation",
                Dish = dish,
                Table = table,
                OrderPrice = orderPrice
            };

            this._dbContext.Orders.Add(newOrder);
            await this._dbContext.SaveChangesAsync(cancellationToken);

            return newOrder;
        }

        public async Task MarkAsDelivered(int orderId, CancellationToken cancellationToken)
        {
            Order order = await _dbContext.Orders.FirstOrDefaultAsync(s => s.Id == orderId, cancellationToken);

            order.Status = "delivered";

            await this._dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteOrder(int id, CancellationToken cancellationToken)
        {
            var order = await this._dbContext.Orders.FirstOrDefaultAsync(x=>x.Id == id, cancellationToken) ?? throw new ApplicationException("Table with that id doesn't exists.");

            this._dbContext.Orders.Remove(order);
            await this._dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
