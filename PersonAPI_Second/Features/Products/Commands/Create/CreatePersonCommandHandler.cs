using MediatR;
using PersonAPI_Second.Domain;
using PersonAPI_Second.Persistence;

public class CreatePersonCommandHandler(AppDbContext context) : IRequestHandler<CreatePersonCommand, Guid>
{
    public async Task<Guid> Handle(CreatePersonCommand command, CancellationToken cancellationToken)
    {
        var person = new Person(command.FirstName, command.LastName, command.DOB, command.Address);

        await context.Persons.AddAsync(person);
        await context.SaveChangesAsync();

        return person.Id;
    }
}
