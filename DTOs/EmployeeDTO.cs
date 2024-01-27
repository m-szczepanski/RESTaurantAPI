using System.ComponentModel.DataAnnotations;

namespace RESTaurantAPI.DTOs
{
    public class EmployeeDTO
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
    }
}
