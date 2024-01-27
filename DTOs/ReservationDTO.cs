using System;
using RESTaurantAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace RESTaurantAPI.DTOs
{
    public class ReservationDto
    {
        [Key]
        public int Id { get; set; }

        public DateOnly Date { get; set; }

        public DateTime Hour { get; set; }

        public int TableId { get; set; }
    }
}
