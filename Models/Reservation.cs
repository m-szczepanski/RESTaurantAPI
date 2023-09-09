﻿using System.ComponentModel.DataAnnotations;

namespace RESTaurantAPI.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        public DateOnly Date { get; set; }

        public DateTime Hour { get; set; }

        public virtual Table Table { get; set; }

    }
}
