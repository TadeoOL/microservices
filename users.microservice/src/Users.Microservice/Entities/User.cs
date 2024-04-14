using Common;

namespace Users.Microservice.Entities;

public class User : IEntity
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? BirthDay { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
