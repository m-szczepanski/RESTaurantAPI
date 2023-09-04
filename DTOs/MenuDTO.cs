using System.ComponentModel.DataAnnotations;

namespace RESTaurantAPI.DTOs
{
    public class MenuDTO
    {
        [Key]
        public int Id { get; set; }

        public string DishName { get; set; }
        public string Quisine { get; set; }
        public string Ingredients { get; set; }
        public bool Vegetarian { get; set; }
        public bool Vegan { get; set; }
    }
}
