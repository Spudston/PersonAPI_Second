using MediatR;

// Used for UpdatePersonCommanHandler- Id is not needed on the frontend due to the Id coming from route
public record UpdatePersonWithIdCommand(Guid Id, string FirstName, string LastName, DateOnly DOB, string Address) : IRequest<Guid>;