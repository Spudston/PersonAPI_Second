using MediatR;

public record CreatePersonCommand(string FirstName, string LastName, DateOnly DOB, string Address) : IRequest<Guid>;
