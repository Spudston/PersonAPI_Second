using Azure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using PersonAPI_Second.Domain;
using PersonAPI_Second.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PersonTests
{
    public class PersonIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> factory;

        public PersonIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task GetPerson_ReturnsPerson_WhenPersonExists()
        {
            // Arrange
            var client = factory.CreateClient();

            // Create a new person with a specific ID for the test
            var personId = Guid.NewGuid();
            var person = new Person
            {
                Id = personId,
                FirstName = "Homer",
                LastName = "Simpson",
                DOB = DateOnly.FromDateTime(new DateTime(1960, 12, 10)),
                Address = "742 Evergreen Terrace"
            };

            // Use a test scope to add the person to the in-memory database
            using (var scope = factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Persons.Add(person);
                await dbContext.SaveChangesAsync();
            }

            // Act
            var response = await client.GetAsync($"/api/Person/{personId}");

            // Assert
            response.EnsureSuccessStatusCode(); // Verifies a 2xx status code
            var responsePerson = await response.Content.ReadFromJsonAsync<Person>();

            Assert.NotNull(responsePerson);
            Assert.Equal("Homer", responsePerson.FirstName);
            Assert.Equal("Simpson", responsePerson.LastName);
        }

        [Fact]
        public async Task GetPerson_ReturnsNotFound_WhenPersonDoesNotExist()
        {
            // Arrange
            var client = factory.CreateClient();
            // Use a non-existing ID for the test
            var nonExistingId = Guid.NewGuid();
            // Act
            var response = await client.GetAsync($"/api/Person/{nonExistingId}");
            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task UpdatePerson_ReturnsChangedPerson_WhenPersonExists()
        {
            // Arrange
            var client = factory.CreateClient();

            // Create a new person with a specific ID for the test
            var personId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            var person = new Person
            {
                Id = personId,
                FirstName = "Homer",
                LastName = "Simpson",
                DOB = DateOnly.FromDateTime(new DateTime(1960, 12, 10)),
                Address = "742 Evergreen Terrace"
            };

            // Use a test scope to add the person to the in-memory database
            using (var scope = factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Persons.Add(person);
                await dbContext.SaveChangesAsync();
            }

            var updatedDOB = DateOnly.FromDateTime(new DateTime(1981, 5, 12));

            var updatedPerson= new UpdatePersonCommand(personId, "Bart", "Simpson", updatedDOB, "742 Evergreen Terrace");

            var updatedResponse = await client.PatchAsJsonAsync($"/api/Person/{personId}", updatedPerson);

            updatedResponse.EnsureSuccessStatusCode();

            var getResponse = await client.GetAsync($"/api/Person/{personId}");
            getResponse.EnsureSuccessStatusCode();
            var finalPerson = await getResponse.Content.ReadFromJsonAsync<Person>();

            Assert.NotNull(finalPerson);
            Assert.Equal("Bart", finalPerson.FirstName);
            Assert.Equal(updatedDOB, finalPerson.DOB);
            Assert.Equal(person.LastName, finalPerson.LastName);
        }

        [Fact]
        public async Task UpdatePerson_ReturnsError_WhenInvalidInput()
        {
            // Arrange
            var client = factory.CreateClient();

            // Create a new person with a specific ID for the test
            var personId = Guid.Parse("00000000-0000-0000-0000-000000000002");
            var person = new Person
            {
                Id = personId,
                FirstName = "Homer",
                LastName = "Simpson",
                DOB = DateOnly.FromDateTime(new DateTime(1960, 12, 10)),
                Address = "742 Evergreen Terrace"
            };

            // Use a test scope to add the person to the in-memory database
            using (var scope = factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Persons.Add(person);
                await dbContext.SaveChangesAsync();
            }

            var updatedDOB = DateOnly.FromDateTime(new DateTime(2100, 5, 12));

            var invalidPerson = new UpdatePersonCommand(personId, "Bart", "Simpson", updatedDOB, "742 Evergreen Terrace");

            var response = await client.PatchAsJsonAsync($"/api/Person/{personId}", invalidPerson);

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}

