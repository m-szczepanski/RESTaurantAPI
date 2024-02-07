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

        public async Task<Menu> AddMenu(List<int> dishIds, string startDateString, string endDateString)
        {
            DateTime startDate = DateTime.Parse(startDateString);
            DateTime endDate = DateTime.Parse(endDateString);
            List<Dish> dishes = await _dbContext.Dishes.Where(d => dishIds.Contains(d.Id)).ToListAsync();

            var newMenu = new Menu
            {
                Dishes = dishes,
                StartDate = startDate,
                EndDate = endDate
            };

            _dbContext.Menus.Add(newMenu);
            await _dbContext.SaveChangesAsync();

            return newMenu;
        }

        public async Task UpdateStartDate(int id, DateTime startDate, CancellationToken cancellationToken)
        {
            Menu menu = await _dbContext.Menus.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

            menu.StartDate = startDate;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateEndDate(int id, DateTime endDate, CancellationToken cancellationToken)
        {
            Menu menu = await _dbContext.Menus.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

            menu.EndDate = endDate;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteMenu(int id, CancellationToken cancellationToken)
        {
            var menu = await _dbContext.Menus.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (menu == null)
            {
                throw new ApplicationException("Menu with that id doesn't exists.");
            }

            _dbContext.Menus.Remove(menu);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
