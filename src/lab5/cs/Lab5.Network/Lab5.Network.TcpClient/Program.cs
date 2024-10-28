using System.Numerics;
using Lab5.Network.Common;
using Lab5.Network.Common.UserApi;

internal class Program
{
    private static object _locker = new object();

    public static async Task Main(string[] args)
    {
        var serverAdress = new Uri("tcp://127.0.0.1:5555");
        var client = new NetTcpClient(serverAdress);
        Console.WriteLine($"Connect to server at {serverAdress}");
        await client.ConnectAsync();

        var userApi = new UserApiClient(client);
        await ManageUsers(userApi);
        client.Dispose();
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
                var addUser = new User(Id: 0,
                    Name : addUserName,
                    Age : 30,
                    Active : true
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
