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


        /// This doesn't work because it has the Guid changing each time the code is run- find a way to prevent a new instance of an entity being made
        /// if a previous instance has the exact same details, maybe- but this could be problematic in practice due to multiple people potentially having the same exact
        /// details (even if it's borderline impossible)
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Person>().HasKey(x => x.Id);
        //    modelBuilder.Entity<Person>().HasData(
        //        new Person("Casper", "Mohabaty", DateOnly.FromDateTime(DateTime.Parse("2005-04-16")), "100 Charming Avenue"),
        //        new Person("Flerbert", "Schminkledorf", DateOnly.FromDateTime(DateTime.Parse("2000-01-01")), "Wazzaaaaaa")
        //        );
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseInMemoryDatabase("CasperDB"); // Change to SQL Database
        //}
    }
}
