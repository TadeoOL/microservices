using System.ComponentModel.DataAnnotations;

namespace Users.Microservice.Dtos;

public record UserDto(
    Guid Id,
    string Name,
    string LastName,
    string BirthDay,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt
);

public record CreateUserDto(
    [Required] string Name,
    [Required] string LastName,
    [Required] string BirthDay
);

public record UpdateUserDto(
    [Required] string Name,
    [Required] string LastName,
    [Required] string BirthDay,
    [Required] DateTimeOffset UpdatedAt
);
