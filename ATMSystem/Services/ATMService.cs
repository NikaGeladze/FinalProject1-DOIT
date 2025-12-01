using ATMSystem.Models;
using ATMSystem.Repository;

namespace ATMSystem.Services;

public class ATMService
{
     private readonly UserService _userService;
     private readonly FileRepository _repository;

     public ATMService(UserService userService, FileRepository repository)
     {
        _userService = userService;
        _repository = repository;
     }

     public decimal CheckBalance(User user)
     { 
         LogTransaction(user, "შეამოწმა ბალანსი", null, user.Balance);
         return user.Balance;
     }

     public void Deposit(User user, decimal amount)
     {
         if (amount <= 0)
            throw new InvalidOperationException("Deposit amount must be greater than zero.");

         user.Balance += amount;
         _userService.UpdateUser(user);
         LogTransaction(user, $"შეავსო ბალანსი {amount} ლარით", amount, user.Balance);
     }

     public void Withdraw(User user, decimal amount)
     {
         if (amount <= 0)
             throw new InvalidOperationException("Withdrawal amount must be greater than zero.");

         if (user.Balance < amount)
             throw new InvalidOperationException("Insufficient funds.");

          user.Balance -= amount;
          _userService.UpdateUser(user);
          LogTransaction(user, $"გაანაღდა {amount} ლარით", amount, user.Balance);
     }

     public List<TransactionLog> GetTransactionHistory()
     {
           return _repository.LoadUserTransactions();
     }

     private void LogTransaction(User user, string operation, decimal? amount, decimal currentBalance)
     {
          var log = new TransactionLog
           {
                UserName = $"{user.FirstName} {user.LastName}",
                Operation = operation,
                Amount = amount,
                CurrentBalance = currentBalance,
                Date = DateTime.Now
           };
          _repository.SaveTransactionLog(log);
     }
}