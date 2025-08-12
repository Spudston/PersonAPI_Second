using Microsoft.EntityFrameworkCore;
using PersonAPI_Second.Domain;

namespace PersonAPI_Second.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Id);

                // Foundation for auto-incrementing Id displayed instead of Guid. It is allocated but the table is not ordered by DisplayId
                entity.Property(e => e.DisplayId)
                    .ValueGeneratedOnAdd();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
