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
