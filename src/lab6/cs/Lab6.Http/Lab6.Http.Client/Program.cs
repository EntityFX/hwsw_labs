
using Lab6.Http.Common;

internal class Program
{
    private static object _locker = new object();

    public static async Task Main(string[] args)
    {
        var httpClient = new HttpClient() {
            BaseAddress = new Uri("http://localhost:5214/api/")
        };

        var userApiClient = new UserApiClient(httpClient);

        await ManageUsers(userApiClient);
    }

    private static async Task ManageUsers(IUserApi userApi)
    {
        PrintMenu();

        while(true) {
            var key = Console.ReadKey(true);

            PrintMenu();

            if (key.Key == ConsoleKey.D1) 
            {
                var users = await userApi.GetAllAsync();
                Console.WriteLine($"| Id    |     Name        | Active |");
                foreach (var user in users)
                {
                    Console.WriteLine($"| {user.Id,5} | {user.Name,15} | {user.Active,8} |");
                }
            }

            if (key.Key == ConsoleKey.D2) 
            {
                Console.Write("Enter user id: ");
                var userIdString = Console.ReadLine();
                int.TryParse(userIdString, out var userId);
                var user = await userApi.GetAsync(userId);
                Console.WriteLine($"Id={user?.Id}, Name={user?.Name}, Active={user?.Active}");
            }

            if (key.Key == ConsoleKey.D3) 
            {
                Console.Write("Enter user name: ");
                var addUserName = Console.ReadLine() ?? "empty";
                var addUser = new User(id: 0,
                    name : addUserName,
                    age : 30,
                    active : true
                );
                var addResult = await userApi.AddAsync(addUser);

                Console.WriteLine(addResult ? "Ok" : "Error");
            }

            if (key.Key == ConsoleKey.Escape)
            {
                break;
            }
        }
        Console.ReadKey();
        //while (Console.Read)
    }

    private static void PrintMenu()
    {
        lock (_locker)
        {
            Console.WriteLine("1 - Get all users");
            Console.WriteLine("2 - Get user by id");
            Console.WriteLine("3 - Create user");
            Console.WriteLine("4 - Update user");
            Console.WriteLine("5 - Delete user");
            Console.WriteLine("-------");
        }
    }
}