using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class AppDbContext : DbContext
{
    public DbSet<Conference> Conferences { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<SessionParticipant> SessionParticipants { get; set; }
      
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}
