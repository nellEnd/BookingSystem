using BookingSystem.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BookingSystem.Infrastructure.Data
{
    public class BookingSystemContext(DbContextOptions<BookingSystemContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<LoginCredential> LoginCredentials { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
