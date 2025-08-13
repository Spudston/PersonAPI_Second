using MediatR;
using PersonAPI_Second.Persistence;

public class UpdatePersonCommandHandler(AppDbContext context) : IRequestHandler<UpdatePersonWithIdCommand, Guid>
{
    public async Task<Guid> Handle(UpdatePersonWithIdCommand command, CancellationToken cancellationToken)
    {
        var person = await context.Persons.FindAsync(command.Id);

        if (person == null) return Guid.Empty;

        if (person.FirstName != null)
            person.FirstName = command.FirstName;

        if (person.LastName != null)
            person.LastName = command.LastName;

        if (person.DOB != null)
            person.DOB = command.DOB;

        if (person.Address != null)
            person.Address = command.Address;

        await context.SaveChangesAsync();

        return person.Id;
    }
}
