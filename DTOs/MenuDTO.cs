using System.ComponentModel.DataAnnotations;
using RESTaurantAPI.Models;

namespace RESTaurantAPI.DTOs
{
    public class MenuDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public List<Dish> Dishes { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
