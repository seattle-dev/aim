using Microsoft.EntityFrameworkCore;
using ElevatorAPI.Models;

namespace ElevatorAPI.Data
{
    public class ElevatorDbContext : DbContext
    {
        public ElevatorDbContext(DbContextOptions<ElevatorDbContext> options)
            : base(options)
        {
        }

        public DbSet<FloorRequest> FloorRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FloorRequest>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Floor).IsRequired();
                entity.Property(e => e.RequestedAt).IsRequired();
            });
        }
    }
}
