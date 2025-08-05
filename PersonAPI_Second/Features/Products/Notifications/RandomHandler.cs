using MediatR;

public class RandomHandler(ILogger<RandomHandler> logger) : INotificationHandler<PersonCreatedNotification>
{
    public Task Handle(PersonCreatedNotification notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling notification for person creation with id : {notification.Id}. performing random action.");

        return Task.CompletedTask;
    }
}