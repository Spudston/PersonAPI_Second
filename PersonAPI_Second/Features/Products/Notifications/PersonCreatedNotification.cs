using MediatR;

public record PersonCreatedNotification(Guid Id) : INotification;