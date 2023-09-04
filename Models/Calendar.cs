using System.ComponentModel.DataAnnotations;

namespace RESTaurantAPI.Models
{
    public class Calendar
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }
    }
}
