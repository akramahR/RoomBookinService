using Microsoft.EntityFrameworkCore;

namespace RoomBookinService.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, Type = "Single", Price = 75, IsAvailable = true },
                new Room { Id = 2, Type = "Double", Price = 100, IsAvailable = true },
                new Room { Id = 3, Type = "Suite", Price = 150, IsAvailable = true },
                new Room { Id = 4, Type = "Single", Price = 80, IsAvailable = true },
                new Room { Id = 5, Type = "Double", Price = 120, IsAvailable = true },
                new Room { Id = 6, Type = "Suite", Price = 200, IsAvailable = true },
                new Room { Id = 7, Type = "Single", Price = 70, IsAvailable = true },
                new Room { Id = 8, Type = "Double", Price = 110, IsAvailable = true },
                new Room { Id = 9, Type = "Suite", Price = 180, IsAvailable = true },
                new Room { Id = 10, Type = "Single", Price = 85, IsAvailable = true },
                new Room { Id = 11, Type = "Double", Price = 130, IsAvailable = true },
                new Room { Id = 12, Type = "Suite", Price = 220, IsAvailable = true },
                new Room { Id = 13, Type = "Single", Price = 90, IsAvailable = true },
                new Room { Id = 14, Type = "Double", Price = 125, IsAvailable = true },
                new Room { Id = 15, Type = "Suite", Price = 210, IsAvailable = true },
                new Room { Id = 16, Type = "Single", Price = 95, IsAvailable = true },
                new Room { Id = 17, Type = "Double", Price = 115, IsAvailable = false },
                new Room { Id = 18, Type = "Suite", Price = 250, IsAvailable = true },
                new Room { Id = 19, Type = "Single", Price = 80, IsAvailable = true },
                new Room { Id = 20, Type = "Double", Price = 140, IsAvailable = false },
                new Room { Id = 21, Type = "Suite", Price = 230, IsAvailable = true },
                new Room { Id = 22, Type = "Single", Price = 85, IsAvailable = false },
                new Room { Id = 23, Type = "Double", Price = 135, IsAvailable = true },
                new Room { Id = 24, Type = "Suite", Price = 260, IsAvailable = true },
                new Room { Id = 25, Type = "Single", Price = 100, IsAvailable = true },
                new Room { Id = 26, Type = "Double", Price = 150, IsAvailable = true },
                new Room { Id = 27, Type = "Suite", Price = 240, IsAvailable = false },
                new Room { Id = 28, Type = "Single", Price = 95, IsAvailable = true },
                new Room { Id = 29, Type = "Double", Price = 145, IsAvailable = true },
                new Room { Id = 30, Type = "Suite", Price = 300, IsAvailable = true }
                );

        }
    }
}
