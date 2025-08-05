using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonAPI_Second.Persistence;

public class ListProductsQueryHandler(AppDbContext context) : IRequestHandler<ListPersonQuery, List<PersonDto>>
{
    public async Task<List<PersonDto>> Handle(ListPersonQuery request, CancellationToken cancellationToken)
    {
        return await context.Persons
            .OrderBy(e => e.DisplayId) // This outputs the entities in order on the front end but not in the database
            .Select(p => new PersonDto(p.Id, p.FirstName, p.LastName, p.DOB, p.Address))
            .ToListAsync();
    }
}