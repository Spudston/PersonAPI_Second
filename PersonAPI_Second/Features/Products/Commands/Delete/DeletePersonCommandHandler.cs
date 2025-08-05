using MediatR;
using PersonAPI_Second.Persistence;

public class DeletePersonCommandHandler(AppDbContext context) : IRequestHandler<DeletePersonCommand>
{
    public async Task Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await context.Persons.FindAsync(request.Id);

        if (person == null) return;

        context.Persons.Remove(person);
        await context.SaveChangesAsync();
    }
}
