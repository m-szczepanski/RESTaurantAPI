using System;
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

        public async Task<Order> AddOrder(int quantity, int tableId, int dishId, CancellationToken cancellationToken)
        {

            Table table = await _dbContext.Tables.GetTableById(tableId, cancellationToken);
            Dish dish = await _dbContext.Dishes.GetDishById(dishId, cancellationToken);

            var newOrder = new Order
            {
                OrderTime = DateTime.Now,
                Quantity = quantity,
                Status = "in preparation",
                Dish = dish,
                Table = table,
            };

            _dbContext.Orders.Add(newOrder);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return newOrder;
        }

        public async Task MarkAsDelivered(int orderId, CancellationToken cancellationToken)
        {
            Order order = await _dbContext.Orders.FirstOrDefaultAsync(s => s.Id == orderId, cancellationToken);

            order.Status = "delivered";

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
