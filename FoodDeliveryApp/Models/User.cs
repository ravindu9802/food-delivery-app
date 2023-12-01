using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FoodDeliveryApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        [JsonIgnore]
        public virtual List<Restaurant>? Restaurants { get; set; }

    }
}
