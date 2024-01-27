﻿using System.ComponentModel.DataAnnotations;

namespace RESTaurantAPI.DTOs
{
    public class TableDto
    {
        [Key]
        public int Id { get; set; }

        public int Seats { get; set; }
        public bool Availability { get; set; }
    }
}
