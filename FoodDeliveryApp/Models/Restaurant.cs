using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FoodDeliveryApp.Models
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public string BrandName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        [Required]
        public int UserId { get; set; }

        [JsonIgnore]
        public virtual User? User { get; set; }
    }
}
