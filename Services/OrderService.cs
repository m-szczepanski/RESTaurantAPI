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

        public async Task<Order> AddOrder(DateTime orderTime, int quantity, string status, int tableId, int dishId, CancellationToken cancellationToken)
        {

            Table table = await _dbContext.Tables.GetTableById(tableId, cancellationToken);
            Dish dish = await _dbContext.Dishes.GetDishById(dishId, cancellationToken);

            var newOrder = new Order
            {
                OrderTime = orderTime,
                Quantity = quantity,
                Status = status,
                Dish = dish,
                Table = table,
            };

            _dbContext.Orders.Add(newOrder);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return newOrder;
        }
    }
}
