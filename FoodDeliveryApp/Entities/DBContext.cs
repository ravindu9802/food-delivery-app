using FoodDeliveryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryApp.Entities
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
            //modelBuilder.Entity<User>()
            //    .HasMany(u => u.Restaurants)
            //    .WithOne(u => u.User)
            //    .HasForeignKey(c => c.UserId);
        //}

        public DbSet<User> Users { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }

    }
}
