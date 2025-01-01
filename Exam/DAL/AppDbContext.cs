using Microsoft.EntityFrameworkCore;

namespace DAL;

public class AppDbContext : DbContext
{
    // public DbSet<Booking> Bookings { get; set; }
    // public DbSet<Client> Clients { get; set; }
    // public DbSet<Table> Tables { get; set; }
    // public DbSet<Restaurant> Restaurants { get; set; }
      
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}
