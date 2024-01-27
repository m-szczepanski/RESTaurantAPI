using System.ComponentModel.DataAnnotations;

namespace RESTaurantAPI.Models
{
    public class Table
    {
        [Key]
        public int Id { get; set; }

        public int Seats { get; set; }
        public bool Availability { get; set; }

        public static explicit operator Table(List<Table> v)
        {
            throw new NotImplementedException();
        }
    }
}
