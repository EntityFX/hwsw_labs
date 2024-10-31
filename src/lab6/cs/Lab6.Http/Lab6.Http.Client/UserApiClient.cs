
using System.Net.Http.Json;
using Lab6.Http.Common;

public class UserApiClient : IUserApi
{
    private readonly HttpClient httpClient;

    public UserApiClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public Task<bool> AddAsync(User newUser)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<User[]> GetAllAsync()
    {
        var results = await httpClient.GetFromJsonAsync<User[]>("User");

        return results?.ToArray() ?? Array.Empty<User>();
    }

    public async Task<User?> GetAsync(int id)
    {
        try 
        {
            var result = await httpClient.GetFromJsonAsync<User?>($"User/{id}");

            return result;
        }
        catch 
        {
            return null;
        }
    }

    public Task<bool> UpdateAsync(int id, User updateUser)
    {
        throw new NotImplementedException();
    }
}
