﻿using System;
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
        private readonly APIDbContext _dbContext;

        public DishService(APIDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<List<Dish>> GetAllDishes(CancellationToken cancellationToken, int? skip = null, int? limit = null)
        {
            var dishes = await this._dbContext.Dishes
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return dishes == null ? throw new ApplicationException("No dishes are in the database right now.") : dishes;
        }

        public async Task<Dish> GetDishById(int dishId, CancellationToken cancellationToken)
        {
            var dish = await this._dbContext.Dishes.Where(x => x.Id == dishId)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            return dish == null ? throw new ApplicationException("No dish was found") : dish;
        }

        public async Task<List<Dish>> GetDishByName(string dishName, CancellationToken cancellationToken)
        {
            var dishes = await this._dbContext.Dishes.Where(x => x.DishName == dishName)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return dishes == null ? throw new ApplicationException("No dish with that name was found") : dishes;
        }

        public async Task<List<Dish>> GetDishesFromCuisine(string cuisine, CancellationToken cancellationToken)
        {
            var dishes = await this._dbContext.Dishes.Where(x => x.Cuisine == cuisine)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return dishes == null ? throw new ApplicationException("No dishes with that cuisine were found") : dishes;
        }
        
        public async Task<List<Dish>> GetVeganDishes(CancellationToken cancellationToken)
        {
            var dishes = await this._dbContext.Dishes.Where(x => x.Vegan == true)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return dishes == null ? throw new ApplicationException("No dishes were found") : dishes;
        }

        public async Task<List<Dish>> GetVegetarianDishes(CancellationToken cancellationToken)
        {
            var dishes = await this._dbContext.Dishes.Where(x => x.Vegetarian == true)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return dishes == null ? throw new ApplicationException("No dishes were found") : dishes;
        }

        public async Task<List<Dish>> GetSpicyDishes(CancellationToken cancellationToken)
        {
            var dishes = await this._dbContext.Dishes.Where(x => x.Spicy == true)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return dishes == null ? throw new ApplicationException("No dishes were found") : dishes;
        }

        public async Task<List<Dish>> GetDishesByParameters(string dishName = null, string allergens = null, decimal? maxPrice = null,
            string cuisine = null, bool? vegetarian = null, bool? vegan = null, bool? spicy = null, CancellationToken cancellationToken = default)
        {
            IQueryable<Dish> query = this._dbContext.Dishes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(dishName))
            {
                query = query.Where(d => d.DishName.Contains(dishName));
            }

            if (!string.IsNullOrWhiteSpace(allergens))
            {
                query = query.Where(d => d.Allergens.Contains(allergens));
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(d => d.Price <= maxPrice.Value);
            }

            if (!string.IsNullOrWhiteSpace(cuisine))
            {
                query = query.Where(d => d.Cuisine.Equals(cuisine, StringComparison.OrdinalIgnoreCase));
            }

            if (vegetarian.HasValue)
            {
                query = query.Where(d => d.Vegetarian == vegetarian.Value);
            }

            if (vegan.HasValue)
            {
                query = query.Where(d => d.Vegan == vegan.Value);
            }

            if (spicy.HasValue)
            {
                query = query.Where(d => d.Spicy == spicy.Value);
            }

            var result = await query.ToListAsync(cancellationToken);

            if (result == null || result.Count == 0)
            {
                throw new ApplicationException("No dishes match the specified criteria.");
            }

            return result;
        }

        public async Task<Dish> AddDish(string dishName, string? allergens, decimal price, string cuisine, bool vegetarian, bool vegan, bool spicy, CancellationToken cancellationToken)
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

            this._dbContext.Dishes.Add(newDish);
            await this._dbContext.SaveChangesAsync(cancellationToken);

            return newDish;
        }

        public async Task UpdateDish(int dishId, string dishName, string allergens, decimal price, string cuisine, bool vegetarian, bool vegan, bool spicy, CancellationToken cancellationToken)
        {
            Dish dish = await _dbContext.Dishes.FirstOrDefaultAsync(s => s.Id == dishId, cancellationToken);

            dish.DishName = dishName;
            dish.Allergens = allergens;
            dish.Price = price;
            dish.Cuisine = cuisine;
            dish.Vegetarian = vegetarian;
            dish.Vegan = vegan;
            dish.Spicy = spicy;

            await this._dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteDish(int dishId, CancellationToken cancellationToken)
        {
            var dish = await this._dbContext.Dishes.FirstOrDefaultAsync(x => x.Id == dishId, cancellationToken) ?? throw new ApplicationException("Dish with that id doesn't exist.");

            this._dbContext.Dishes.Remove(dish);
            await this._dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
