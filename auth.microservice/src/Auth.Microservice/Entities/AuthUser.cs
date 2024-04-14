using Common;

namespace Auth.Microservice.Entities;

public class AuthUser : IEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
