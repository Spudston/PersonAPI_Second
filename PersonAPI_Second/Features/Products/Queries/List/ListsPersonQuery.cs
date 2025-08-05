using MediatR;

public record ListPersonQuery : IRequest<List<PersonDto>>;