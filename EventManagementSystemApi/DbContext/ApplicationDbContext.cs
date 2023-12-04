namespace EventManagementSystemApi.DbContext
{
    using EventManagementSystemApi.Models;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Eventes { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure your entity mappings and relationships here
            modelBuilder.Entity<User>()
                .HasMany(u => u.Ticketes)
                .WithOne(p => p.user)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Eventes)
                .WithOne(p => p.user)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Event>()
                .HasMany(u => u.Ticket)
                .WithOne(p => p.Event)
                .HasForeignKey(p => p.EventId);

        }
    }
}
