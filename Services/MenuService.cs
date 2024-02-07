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
    public class MenuService
    {
        private readonly APIDbContext _dbContext;

        public MenuService(APIDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<List<Menu>> GetAll(CancellationToken cancellationToken, int? skip = null, int? limit = null)
        {
            var menus = await _dbContext.Menus
                .Include(x=>x.Dishes)
                .ToListAsync(cancellationToken);

            return menus == null ? throw new ApplicationException("No orders are in the database right now.") : menus;
        }

        public async Task<Menu> AddMenu(List<int> dishIds, string dateString)
        {
            DateOnly date = DateOnly.Parse(dateString);
            List<Dish> dishes = await _dbContext.Dishes.Where(d => dishIds.Contains(d.Id)).ToListAsync();

            var newMenu = new Menu
            {
                Dishes = dishes,
                Date = date
            };

            _dbContext.Menus.Add(newMenu);
            await _dbContext.SaveChangesAsync();

            return newMenu;
        }
    }
}
