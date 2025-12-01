using ATMSystem.Repository;
using ATMSystem.Services;
using ATMSystem.UI;

namespace ATMSystem;

class Program
{
    static void Main(string[] args)
    {
        var repository = new FileRepository("../../../Logs/users.json",@"../../../Logs/userlogs.json");
        var userService = new UserService(repository);
        var atmService = new ATMService(userService, repository);
        var console = new ATMConsole(userService, atmService);

        console.Run();
    }
}