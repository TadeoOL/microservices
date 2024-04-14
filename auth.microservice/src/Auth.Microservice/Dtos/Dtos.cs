using System.ComponentModel.DataAnnotations;

namespace Auth.Microservice.Dtos;

public record CreateUserAuthDto(string Email, string Password);

public record AuthUserDto(
    Guid UserId,
    [Required] string Email,
    [Required] string Password,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt
);

public record CreateUserDto(Guid Id, DateTimeOffset CreatedAt, DateTimeOffset UpdatedAt);

public record UserDto(Guid Id, string Name, string LastName);
