using RESTaurantAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace RESTaurantAPI.DTOs
{
    public class OrdersDTO
    {
        [Key]
        public int Id { get; set; }

        public int CalendarId { get; set; }
        public virtual int TableId { get; set; }
        public int MenuId { get; set; }
        public DateTime OrderTime { get; set; }
        public int Quantity { get; set; }
    }
}
