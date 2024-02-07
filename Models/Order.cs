using System;
using System.ComponentModel.DataAnnotations;

namespace RESTaurantAPI.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal OrderPrice { get; set; }

        public DateTime OrderTime { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public virtual Table Table { get; set; }
        public Dish Dish { get; set; }
    }
}
