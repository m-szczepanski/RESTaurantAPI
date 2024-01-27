using System.ComponentModel.DataAnnotations;

namespace RESTaurantAPI.DTOs
{
    public class MenuDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string DishName { get; set; }
        public string Quisine { get; set; }
        [Required]
        public string[] Ingredients { get; set; }
        public bool Vegetarian { get; set; }
        public bool Vegan { get; set; }
        public bool Spicy { get; set; }
    }
}
