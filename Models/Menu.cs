﻿using System;
using System.ComponentModel.DataAnnotations;

namespace RESTaurantAPI.Models
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public List<Dish> Dishes { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
