using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RESTaurantAPI.Data;
using Microsoft.EntityFrameworkCore;
using RESTaurantAPI.Models;


namespace RESTaurantAPI.Services
{
    public class DishService
    {
        private readonly APIDbContext dbContext;

        public DishService(APIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Dish>> GetAllDishes(CancellationToken cancellationToken, int? skip = null, int? limit = null)
        {
            var dishes = await dbContext.Dishes
                .ToListAsync(cancellationToken);

            return dishes == null ? throw new ApplicationException("No dishes are in the database right now.") : dishes;
        }

        public async Task<Dish> GetDishById(int dishId, CancellationToken cancellationToken)
        {
            var dish = await dbContext.Dishes.Where(x => x.Id == dishId).FirstOrDefaultAsync(cancellationToken);

            return dish == null ? throw new ApplicationException("No employee found") : dish;
        }


        public async Task<Dish> AddDish(string dishName, string[] allergens, decimal price, string cuisine, bool vegetarian, bool vegan, bool spicy, CancellationToken cancellationToken)
        {
            var newDish = new Dish
            {
                DishName = dishName,
                Allergens = allergens,
                Price = price,
                Cuisine = cuisine,
                Vegetarian = vegetarian,
                Vegan = vegan,
                Spicy = spicy
            };

            dbContext.Dishes.Add(newDish);
            await dbContext.SaveChangesAsync(cancellationToken);

            return newDish;
        }

        public async Task UpdateDish(int dishId, string dishName, string[] allergens, decimal price, string cuisine, bool vegetarian, bool vegan, bool spicy, CancellationToken cancellationToken)
        {
            Dish dish = await dbContext.Dishes.FirstOrDefaultAsync(s => s.Id == dishId, cancellationToken);

            dish.DishName = dishName;
            dish.Allergens = allergens;
            dish.Price = price;
            dish.Cuisine = cuisine;
            dish.Vegetarian = vegetarian;
            dish.Vegan = vegan;
            dish.Spicy = spicy;

            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteDish(int dishId, CancellationToken cancellationToken)
        {
            var dish = await dbContext.Dishes.FirstOrDefaultAsync(x => x.Id == dishId, cancellationToken);

            if (dish == null)
            {
                throw new ApplicationException("Dish with that id doesn't exists.");
            }

            dbContext.Dishes.Remove(dish);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
