using Microsoft.EntityFrameworkCore;
using UNVICal.Models;

namespace UNVICal.Data
{
    // Specifičan DbContext za Evente
    public class EventsDbContext : DbContext
    {
        public EventsDbContext(DbContextOptions<EventsDbContext> options) : base(options) { }

        // DbSet koji mapira Event model na tabelu u bazi
        public DbSet<Event> Events { get; set; }
    }
}
