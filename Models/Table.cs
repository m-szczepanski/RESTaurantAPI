using System.ComponentModel.DataAnnotations;

namespace RESTaurantAPI.Models
{
    public class Table
    {
        [Key]
        public int Id { get; set; }

        public int Seats { get; set; }
        public bool Available { get; set; }
    }
}
