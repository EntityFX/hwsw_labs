namespace Lab6.Http.Common;

public interface IUserApi 
{
    Task<bool> AddAsync(User newUser);

    Task<bool> DeleteAsync(int id);

    Task<bool> UpdateAsync(int id, User updateUser);

    Task<User?> GetAsync(int id);

    Task<User[]> GetAllAsync();
}