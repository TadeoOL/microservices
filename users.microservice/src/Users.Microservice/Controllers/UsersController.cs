using Common;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using User.Microservice.Contracts;
using Users.Microservice.Dtos;
using Users.Microservice.Entities;

namespace Users.Microservice.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IRepository<UserClass> usersRepository;
    private readonly IPublishEndpoint publishEndpoint;

    public UsersController(IRepository<UserClass> usersRepository, IPublishEndpoint publishEndpoint)
    {
        this.usersRepository = usersRepository;
        this.publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    public async Task<IEnumerable<UserDto>> GetAsync()
    {
        var users = (await usersRepository.GetAllAsync()).Select(user => user.AsDto());
        return users;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetByIdAsync(Guid id)
    {
        var user = await usersRepository.GetAsync(id);
        if (user == null)
            return NotFound();
        return user.AsDto();
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> PostAsync(UserDto userDto)
    {
        var user = new UserClass()
        {
            Name = userDto.Name,
            LastName = userDto.LastName,
            BirthDay = userDto.BirthDay,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };
        await usersRepository.CreateAsync(user);
        await publishEndpoint.Publish(
            new UserCreated(
                user.Id,
                user.Name,
                user.LastName,
                user.BirthDay,
                user.CreatedAt,
                user.UpdatedAt
            )
        );
        return CreatedAtAction(nameof(GetByIdAsync), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(Guid id, UserDto userDto)
    {
        var user = await usersRepository.GetAsync(id);
        if (user == null)
            return NotFound();
        user.Name = userDto.Name;
        user.LastName = userDto.LastName;
        user.BirthDay = userDto.BirthDay;
        user.UpdatedAt = DateTimeOffset.UtcNow;
        await usersRepository.UpdateAsync(user);
        await publishEndpoint.Publish(
            new UserUpdated(
                user.Id,
                user.Name,
                user.LastName,
                user.BirthDay,
                user.CreatedAt,
                user.UpdatedAt
            )
        );
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var user = await usersRepository.GetAsync(id);
        if (user == null)
            return NotFound();
        await usersRepository.RemoveAsync(user.Id);
        await publishEndpoint.Publish(new UserDeleted(user.Id));
        return NoContent();
    }
}
