using MediatR;
using PersonAPI_Second.Persistence;

public class GetPersonQueryHandler(AppDbContext context)
    : IRequestHandler<GetPersonQuery, PersonDto?>
{
    public async Task<PersonDto?> Handle(GetPersonQuery request, CancellationToken cancellationToken)
    {
        var person = await context.Persons.FindAsync(request.Id);

        if (person == null)
        {
            return null;
        }

        return new PersonDto(person.Id, person.FirstName, person.LastName, person.DOB, person.Address);
    }
}