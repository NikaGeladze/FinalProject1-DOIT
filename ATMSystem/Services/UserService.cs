using ATMSystem.Models;
using ATMSystem.Repository;

namespace ATMSystem.Services;

public class UserService
{
    private readonly FileRepository _repository;
    private List<User> _users;

    public UserService(FileRepository repository)
    {
      _repository = repository;
      _users = _repository.LoadUsers();
    }

    public User RegisterUser(string firstName, string lastName, string personalId, string password)
    {
            if (_users.Any(u => u.PersonalId == personalId))
                throw new InvalidOperationException("User with this personal ID already exists.");

            if (_users.Any(u => u.Password == password))
                throw new InvalidOperationException("This password is already taken.");

            if (password.Length != 4 || !password.All(char.IsDigit))
                throw new InvalidOperationException("Password must be exactly 4 digits.");
            if (personalId.Length <= 0) throw new InvalidOperationException("Personal id must not be empty.");
            if (firstName.Length <= 0) throw new InvalidOperationException("Name must not be empty.");
            if (lastName.Length <= 0) throw new InvalidOperationException("Last name must not be empty.");

            int newId = _users.Any() ? _users.Max(u => u.Id) + 1 : 1;

            var newUser = new User
            {
                Id = newId,
                FirstName = firstName,
                LastName = lastName,
                PersonalId = personalId,
                Password = password,
                Balance = 0
            };

            _users.Add(newUser);
            _repository.SaveUsers(_users);
            return newUser;
    }

    public User Authenticate(string personalId, string password)
    {
        var user = _users.FirstOrDefault(u => u.PersonalId == personalId && u.Password == password);
        if (user == null) throw new UnauthorizedAccessException("Invalid personal ID or password.");
        return user;
    }

    public User GetUser(int userId)
    { 
        return _users.FirstOrDefault(u => u.Id == userId);
    }

    public void UpdateUser(User user)
    {
        var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
        if (existingUser != null)
        {
            existingUser.Balance = user.Balance;
            _repository.SaveUsers(_users);
        }
    }
}