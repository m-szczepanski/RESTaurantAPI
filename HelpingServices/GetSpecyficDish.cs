﻿using System.Globalization;
using Microsoft.EntityFrameworkCore;
using RESTaurantAPI.Models;

namespace RESTaurantAPI.HelpingServices
{
    public static class GetSpecyficDish
    {
        public static async Task<Dish> GetDishById(this DbSet<Dish> dishes, int id, CancellationToken cancellationToken)
        {
            Dish dish = await dishes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            return dish;
        }
    }
}