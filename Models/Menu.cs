using System;
using System.ComponentModel.DataAnnotations;

namespace RESTaurantAPI.Models
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Dish Dish { get; set; }
        public DateOnly Date { get; set; }
    }
}
