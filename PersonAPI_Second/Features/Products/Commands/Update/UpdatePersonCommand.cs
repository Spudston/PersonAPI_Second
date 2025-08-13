using MediatR;

public record UpdatePersonCommand(string FirstName, string LastName, DateOnly DOB, string Address) : IRequest<Guid>;