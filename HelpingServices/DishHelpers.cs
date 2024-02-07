using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RESTaurantAPI.Models;

namespace RESTaurantAPI.HelpingServices
{
    public static class DishHelpers
    {
        public static async Task<Dish> GetDishById(this DbSet<Dish> dishes, int id, CancellationToken cancellationToken)
        {
            Dish dish = await dishes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            return dish == null ? throw new ApplicationException($"There is no dish of {id} id.") : dish;
        }
    }
}
