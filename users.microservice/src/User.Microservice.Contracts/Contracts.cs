namespace User.Microservice.Contracts;

public record UserCreated(
    Guid UserId,
    string Name,
    string LastName,
    string BirthDate,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt
);

public record UserUpdated(
    Guid UserId,
    string Name,
    string LastName,
    string BirthDate,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt
);

public record UserDeleted(Guid UserId);
