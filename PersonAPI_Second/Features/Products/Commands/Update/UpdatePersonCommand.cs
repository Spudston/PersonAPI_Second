using MediatR;

public record UpdatePersonCommand(Guid Id, string FirstName, string LastName, DateOnly DOB, string Address) : IRequest<Guid>;