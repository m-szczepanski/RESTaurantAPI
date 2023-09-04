using System.ComponentModel.DataAnnotations;

namespace RESTaurantAPI.Models
{
    public class Reservations
    {
        [Key]
        public int Id { get; set; }

        public virtual Calendar Calendar { get; set; }
        public virtual Table Table { get; set; }
        public DateTime Hour { get; set; }
    }
}
