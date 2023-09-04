using System.ComponentModel.DataAnnotations;

namespace RESTaurantAPI.Models
{
    public class Orders
    {
        [Key]
        public int Id { get; set; }
        
        public virtual Calendar Calendar { get; set; }
        public virtual Table Table { get; set; }
        public Menu Menu { get; set; }
        public DateTime OrderTime { get; set; }
        public int Quantity { get; set; }
    }
}
