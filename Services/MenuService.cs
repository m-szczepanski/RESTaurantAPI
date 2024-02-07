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
    public class MenuService
    {
        private readonly APIDbContext _dbContext;

        public MenuService(APIDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<List<Menu>> GetAll(CancellationToken cancellationToken, int? skip = null, int? limit = null)
        {
            var menus = await this._dbContext.Menus
                .Include(x=>x.Dishes)
                .ToListAsync(cancellationToken);

            return menus == null ? throw new ApplicationException("No orders are in the database right now.") : menus;
        }

        public async Task<Menu> GetById(int id, CancellationToken cancellationToken)
        {
            var menu = await this._dbContext.Menus.Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);

            return menu == null ? throw new ApplicationException("No menu was found") : menu;
        }

        public async Task<Menu> GetByStartDate(DateTime startDate, CancellationToken cancellationToken)
        {
            var menu = await this._dbContext.Menus.Where(x => x.StartDate == startDate).FirstOrDefaultAsync(cancellationToken);

            return menu == null ? throw new ApplicationException("No menu found") : menu;
        }

        public async Task<List<Menu>> GetBetweenDates(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
        {
            var menus = await this._dbContext.Menus.Where(t => t.StartDate <= startDate && t.EndDate >= endDate)
                .Include(x=>x.Dishes).ToListAsync(cancellationToken);

            return menus == null ? throw new ApplicationException("No menu found") : menus;
        }

        public async Task<Menu> GetCurrentMenu(CancellationToken cancellationToken)
        {
            var todayDate = DateTime.Today;
            var menu = await this._dbContext.Menus.Where(t => t.StartDate <= todayDate && t.EndDate >= todayDate)
                .Include(x=>x.Dishes).FirstOrDefaultAsync(cancellationToken);

            return menu == null ? throw new ApplicationException("There's no current menu in the database.") : menu;
        }

        public async Task<Menu> AddMenu(List<int> dishIds, string startDateString, string endDateString)
        {
            DateTime startDate = DateTime.Parse(startDateString);
            DateTime endDate = DateTime.Parse(endDateString);
            List<Dish> dishes = await this._dbContext.Dishes.Where(d => dishIds.Contains(d.Id)).ToListAsync();

            var newMenu = new Menu
            {
                Dishes = dishes,
                StartDate = startDate,
                EndDate = endDate
            };

            this._dbContext.Menus.Add(newMenu);
            await this._dbContext.SaveChangesAsync();

            return newMenu;
        }

        public async Task UpdateStartDate(int id, DateTime startDate, CancellationToken cancellationToken)
        {
            Menu menu = await this._dbContext.Menus.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

            menu.StartDate = startDate;

            await this._dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateEndDate(int id, DateTime endDate, CancellationToken cancellationToken)
        {
            Menu menu = await this._dbContext.Menus.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

            menu.EndDate = endDate;

            await this._dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteMenu(int id, CancellationToken cancellationToken)
        {
            var menu = await this._dbContext.Menus.FirstOrDefaultAsync(x => x.Id == id, cancellationToken) ?? throw new ApplicationException("Menu with that id doesn't exists.");

            this._dbContext.Menus.Remove(menu);
            await this._dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
