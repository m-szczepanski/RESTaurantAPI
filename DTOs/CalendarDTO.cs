using System.ComponentModel.DataAnnotations;

namespace RESTaurantAPI.DTOs
{
    public class CalendarDTO
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }
    }
}
