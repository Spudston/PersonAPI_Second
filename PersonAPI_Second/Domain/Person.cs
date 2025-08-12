using System.Diagnostics.CodeAnalysis;

namespace PersonAPI_Second.Domain
{
    public class Person
    {
        public Guid Id { get; set; }
        public int DisplayId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateOnly DOB { get; set; }
        public required string Address { get; set; }

        // This is set to public just to make the integration testing simpler for now
        public Person() { }

        [SetsRequiredMembers]
        public Person (string firstName, string lastName, DateOnly dOB, string address)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            DOB = dOB;
            Address = address;
        }
    }
}
