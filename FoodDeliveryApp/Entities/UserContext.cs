using FoodDeliveryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryApp.Entities
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
