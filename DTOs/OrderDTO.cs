using System;
using RESTaurantAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace RESTaurantAPI.DTOs
{
    public class OrderDto
    {
        [Key]
        public int Id { get; set; }

        public DateTime OrderTime { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }

        public virtual int TableId { get; set; }
        public int DishId { get; set; }
        public decimal DishPrice { get; set; }

    }
}
