using MediatR;

public record GetPersonQuery(Guid Id) : IRequest<PersonDto>;