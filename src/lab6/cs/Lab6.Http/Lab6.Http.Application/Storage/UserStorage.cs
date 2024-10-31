using System.Collections.Concurrent;
using Lab6.Http.Common;

class UserStorage : StorageBase<User>, IUserApi
{
    private static readonly Dictionary<int, User> defaultData 
        = new Dictionary<int, User>() {
            [1] = new User(1, "Кот", true, 3),
            [2] = new User(2, "Пёс", true, 5),
            [3] = new User(3, "Гусь", true, 2),
        };

    private static ConcurrentDictionary<int, User> userRepository 
        = new ConcurrentDictionary<int, User>();

    private static int _lastId;

    public UserStorage(IDataSerializer<User[]> dataSerializer) 
        : base(Path.Combine(
            "Data",
            dataSerializer.SerializerType, 
            $"{nameof(User)}.{dataSerializer.SerializerType}"
        ), 
        dataSerializer)
    {
        var readData = ReadAsync().Result;

        userRepository = readData?.Any() == true
            ? new ConcurrentDictionary<int, User>(
                readData.ToDictionary(r => r.Id, r => r))
            : new ConcurrentDictionary<int, User>(defaultData);

        _lastId = userRepository.Count + 1;
    }

    public async Task<bool> AddAsync(User newUser)
    {
        newUser.Id = _lastId;

        if (userRepository.ContainsKey(_lastId)) 
        {
            return false;
        }

        var result = userRepository.TryAdd(_lastId, newUser);

        if (!result) 
        {
            return false;
        }
        Interlocked.Increment(ref _lastId);

        result = await WriteAsync(userRepository.Values.ToArray());

        return result;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (!userRepository.ContainsKey(id)) 
        {
            return false;
        }

        var result = userRepository.Remove(id, out var _);
        if (!result)
        {
            return false;
        }

        result = await WriteAsync(userRepository.Values.ToArray());

        return result;
    }

    public Task<User[]> GetAllAsync()
    {
        return Task.FromResult(userRepository.Values.ToArray());
    }

    public Task<User?> GetAsync(int id)
    {
        if (!userRepository.ContainsKey(id)) 
        {
            return Task.FromResult(default(User));
        }

        return Task.FromResult<User?>(userRepository[id]);
    }

    public async Task<bool> UpdateAsync(int id, User updateUser)
    {
        if (!userRepository.ContainsKey(id)) 
        {
            return false;
        }
        
        var result = userRepository.TryUpdate(id, updateUser, userRepository[id]);
        if (!result)
        {
            return false;
        }

        result = await WriteAsync(userRepository.Values.ToArray());

        return result;
    }
}
