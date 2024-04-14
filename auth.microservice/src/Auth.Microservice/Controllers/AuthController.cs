using Auth.Microservice.Dtos;
using Auth.Microservice.Entities;
using Common;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Microservice.Controllers;

[ApiController]
[Route("authUser")]
public class AuthUsersController : ControllerBase
{
    private readonly IRepository<AuthUser> authUser;

    public AuthUsersController(IRepository<AuthUser> authUser)
    {
        this.authUser = authUser;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthUser>>> GetAsync(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            return BadRequest();
        }

        var items = (await authUser.GetAllAsync(user => user.UserId == userId)).Select(user =>
            user.AsDto()
        );
        return Ok(items);
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync(CreateUserAuthDto createUserAuthDto)
    {
        if (createUserAuthDto.Password == null || createUserAuthDto.Email == null)
        {
            return BadRequest();
        }
        var user = await authUser.GetAllAsync(user => user.Email == createUserAuthDto.Email);
        if (user.Any())
        {
            throw new Exception("Ya existe un usuario registrado con este correo!");
        }
        var newUser = new AuthUser
        {
            Email = createUserAuthDto.Email,
            Password = createUserAuthDto.Password,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };
        await authUser.CreateAsync(newUser);
        return Ok(newUser);
    }
}
