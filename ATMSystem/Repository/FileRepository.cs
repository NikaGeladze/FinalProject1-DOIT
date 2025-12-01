using System.Text;
using System.Text.Json;
using ATMSystem.Models;

namespace ATMSystem.Repository;

public class FileRepository
{
    private readonly string _usersFile;
    private readonly string _logsFile;

    public FileRepository(string usersFile, string logsFile)
    {
        _usersFile = usersFile;
        _logsFile = logsFile;
    }

    public List<User> LoadUsers()
    {
        try
        {
            if (!File.Exists(_usersFile))
                return new List<User>();

            var json = File.ReadAllText(_usersFile);
            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading users: {ex.Message}");
            return new List<User>();
        }
    }

    public void SaveUsers(List<User> users)
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true ,Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping};
            var json = JsonSerializer.Serialize(users, options);
            File.WriteAllText(_usersFile, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving users: {ex.Message}");
        }
    }

    public void SaveTransactionLog(TransactionLog log)
    {
        try
        {
            List<TransactionLog> logs = new List<TransactionLog>();

            if (File.Exists(_logsFile))
            {
                var json = File.ReadAllText(_logsFile);
                logs = JsonSerializer.Deserialize<List<TransactionLog>>(json) ?? new List<TransactionLog>();
            }

            logs.Add(log);
            var options = new JsonSerializerOptions { WriteIndented = true ,Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping};
            var updatedJson = JsonSerializer.Serialize(logs, options);
            File.WriteAllText(_logsFile, updatedJson);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving transaction log: {ex.Message}");
        }
    }

    public List<TransactionLog> LoadUserTransactions()
    {
        try
        {
            if (!File.Exists(_logsFile))
                return new List<TransactionLog>();

            var json = File.ReadAllText(_logsFile);
            var allLogs = JsonSerializer.Deserialize<List<TransactionLog>>(json) ?? new List<TransactionLog>();
            return allLogs;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading transaction history: {ex.Message}");
            return new List<TransactionLog>();
        }

    }
}