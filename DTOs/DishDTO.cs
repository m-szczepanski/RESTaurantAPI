using System.ComponentModel.DataAnnotations;

namespace RESTaurantAPI.DTOs
{
    public class DishDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string DishName { get; set; }
        [Required]
        public string[] Allergens { get; set; }
        [Required]
        public decimal Price { get; set; }

        public string Quisine { get; set; }
        public bool Vegetarian { get; set; }
        public bool Vegan { get; set; }
        public bool Spicy { get; set; }

    }
}
