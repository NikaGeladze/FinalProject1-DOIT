using ATMSystem.Models;
using ATMSystem.Services;

namespace ATMSystem.UI;
public class ATMConsole 
{
    private readonly UserService _userService;
    private readonly ATMService _atmService;
    private User _currentUser;

    public ATMConsole(UserService userService, ATMService atmService)
    {
        _userService = userService;
        _atmService = atmService;
    }

    public void Run()
    {
        while (true)
        {
            try
            {
                Console.WriteLine("=== ATM System ===");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("3. Exit");
                Console.WriteLine("4. Show Transaction History");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Login();
                        break;
                    case "2":
                        Register();
                        break;
                    case "3":
                        return;
                    case "4" :
                        ShowTransactionHistory();
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    private void Login()
    {
        try
        {
            Console.Write("Personal ID: ");
            var personalId = Console.ReadLine();
            Console.Write("Password (4 digits): ");
            var password = Console.ReadLine();

            _currentUser = _userService.Authenticate(personalId, password);
            Console.WriteLine($"Welcome, {_currentUser.FirstName} {_currentUser.LastName}!");
            ATMMenu();
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void Register()
    {
        try
        {
            Console.Write("First Name: ");
            var firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            var lastName = Console.ReadLine();
            Console.Write("Personal ID: ");
            var personalId = Console.ReadLine();
            Console.Write("Password (4 digits): ");
            var password = Console.ReadLine();

            var user = _userService.RegisterUser(firstName, lastName, personalId, password);
            Console.WriteLine($"Registration successful! Your ID: {user.Id}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void ATMMenu()
    {
        while (_currentUser != null)
        {
            try
            {
                Console.Clear();
                Console.WriteLine($"=== Welcome, {_currentUser.FirstName} {_currentUser.LastName} ===");
                Console.WriteLine("1. Check Balance");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Transaction History");
                Console.WriteLine("5. Logout");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CheckBalance();
                        break;
                    case "2":
                        Deposit();
                        break;
                    case "3":
                        Withdraw();
                        break;
                    case "4":
                        ShowTransactionHistory();
                        break;
                    case "5":
                        _currentUser = null;
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        Console.ReadKey();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.ReadKey();
            }
        }
    }

    private void CheckBalance()
    {
        var balance = _atmService.CheckBalance(_currentUser);
        Console.WriteLine($"Your current balance: {balance} GEL");
        Console.ReadKey();
    }

    private void Deposit()
    {
        try
        {
            Console.Write("Enter amount to deposit: ");
            var amount = decimal.Parse(Console.ReadLine());
            _atmService.Deposit(_currentUser, amount);
            Console.WriteLine($"Deposit successful! New balance: {_currentUser.Balance} GEL");
            Console.ReadKey();
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid amount format.");
        }
    }

    private void Withdraw()
    {
        try
        {
            Console.Write("Enter amount to withdraw: ");
            var amount = decimal.Parse(Console.ReadLine());
            _atmService.Withdraw(_currentUser, amount);
            Console.WriteLine($"Withdrawal successful! New balance: {_currentUser.Balance} GEL");
            Console.ReadKey();
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid amount format.");
        }
    }

    private void ShowTransactionHistory()
    {
        var history = _atmService.GetTransactionHistory();
        Console.WriteLine("\n=== Transaction History ===");
        
        if (history.Count == 0)
        {
            Console.WriteLine("No transactions found.");
        }
        else
        {
            foreach (var transaction in history)
            {
                Console.WriteLine($"მომხმარებელმა სახელად {transaction.UserName} - {transaction.Operation} : {transaction.Date:dd.MM.yyyy HH:mm:ss}");
                if (transaction.CurrentBalance.HasValue)
                    Console.WriteLine($"მისი მოქმედი ბალანსი შეადგენს {transaction.CurrentBalance} ლარს");
                Console.WriteLine();
            }
        }
        Console.ReadKey();
    }
}