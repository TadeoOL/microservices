using Auth.Microservice.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Auth.Microservice.Clients;

public class UserClient
{
    private readonly HttpClient httpClient;
    private readonly ILogger<UserClient> logger;

    public UserClient(HttpClient httpClient, ILogger<UserClient> logger)
    {
        this.httpClient = httpClient;
        this.logger = logger;
    }

    public async Task<IReadOnlyCollection<UserDto>> GetUsersAsync(Guid userId)
    {
        try
        {
            var user = await httpClient.GetFromJsonAsync<IReadOnlyCollection<UserDto>>(
                $"/users/{userId}"
            );
            return user!;
        }
        catch (System.Exception)
        {
            throw new Exception("No se encontró al usuario");
        }
    }
}
