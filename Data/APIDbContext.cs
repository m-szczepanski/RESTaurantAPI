﻿using Microsoft.EntityFrameworkCore;
using RESTaurantAPI.Models;

namespace RESTaurantAPI.Data
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}
