using RESTaurantAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace RESTaurantAPI.DTOs
{
    public class ReservationDTO
    {
        [Key]
        public int Id { get; set; }

        public int CalendarId { get; set; }
        public int TableId { get; set; }
        public DateTime Hour { get; set; }
    }
}
