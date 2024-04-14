using Users.Microservice.Dtos;
using Users.Microservice.Entities;

namespace Users.Microservice;

public static class Extensions
{
    public static UserDto AsDto(this User user)
    {
        return new UserDto(
            user.Id,
            user.Name!,
            user.LastName!,
            user.BirthDay!,
            user.CreatedAt,
            user.UpdatedAt
        );
    }
}
