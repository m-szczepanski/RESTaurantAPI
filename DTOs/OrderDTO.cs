using RESTaurantAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace RESTaurantAPI.DTOs
{
    public class OrderDTO
    {
        [Key]
        public int Id { get; set; }

        public DateTime OrderTime { get; set; }
        public int Quantity { get; set; }

        public virtual int TableId { get; set; }
        public int MenuId { get; set; }

    }
}
