using Auth.Microservice.Dtos;
using Auth.Microservice.Entities;

namespace Auth.Microservice;

public static class Extensions
{
    public static AuthUserDto AsDto(this AuthUser authUser)
    {
        return new AuthUserDto(
            authUser.UserId,
            authUser.Email!,
            authUser.Password!,
            authUser.CreatedAt,
            authUser.UpdatedAt
        );
    }
}
