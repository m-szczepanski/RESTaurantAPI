using Microsoft.EntityFrameworkCore;
using RESTaurantAPI.Models;

namespace RESTaurantAPI.Data
{
    public class APIDbContext : DbContext
    {
        public IConfiguration _iConfiguration { get; set; }

        public APIDbContext(IConfiguration iConfiguration)
        {
            _iConfiguration = iConfiguration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_iConfiguration.GetConnectionString("DatabaseConnection"));
        }

        public DbSet<Table> Tables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Dish> Dishes { get; set; }

    }
}
