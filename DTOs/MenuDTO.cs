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
        public string Quisine { get; set; }
        [Required]
        public string[] Ingredients { get; set; }
        public bool Vegetarian { get; set; }
        public bool Vegan { get; set; }
        public bool Spicy { get; set; }
    }
}
