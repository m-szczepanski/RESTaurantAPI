using System;
using System.ComponentModel.DataAnnotations;

namespace RESTaurantAPI.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime Hour { get; set; }

        public int NumberOfSeats { get; set; }
        public virtual Table Table { get; set; }

    }
}
