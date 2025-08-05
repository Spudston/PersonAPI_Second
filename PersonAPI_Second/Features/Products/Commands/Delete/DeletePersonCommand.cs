using MediatR;

public record DeletePersonCommand(Guid Id) : IRequest;