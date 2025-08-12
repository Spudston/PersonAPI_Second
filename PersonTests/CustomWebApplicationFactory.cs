using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonAPI_Second.Domain;
using PersonAPI_Second.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonTests
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // Implement any propietary logic and operations
            builder.ConfigureServices(services =>
            {
                // Remove the existing DbContext registration
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(AppDbContext));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                var dbName = $"TestDb_{Guid.NewGuid()}";

                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer($"Server=LARRYLAPTOP\\MSSQLSERVER01;Database={dbName};Trusted_connection=true;TrustServerCertificate=true");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<AppDbContext>();
                    db.Database.EnsureDeleted();
                    db.Database.Migrate();
                    //SeedDatabase(db);
                }
            });
        }

        //private void SeedDatabase(AppDbContext db)
        //{
        //    db.Persons.AddRange(
        //        new Person
        //        {
        //            FirstName = "Homer",
        //            LastName = "Simpson",
        //            DOB = DateOnly.FromDateTime(new DateTime(1960, 12, 10)),
        //            Address = "742 Evergreen Terrace"
        //        },
        //        new Person
        //        {
        //            FirstName = "Patrick",
        //            LastName = "Star",
        //            DOB = DateOnly.FromDateTime(new DateTime(1992, 10, 11)),
        //            Address = "Rock"
        //        }
        //    );
        //    db.SaveChanges();
        //}
    }
}
