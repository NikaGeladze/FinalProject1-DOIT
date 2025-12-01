namespace ATMSystem.Models;


public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PersonalId { get; set; }
    public string Password { get; set; }
    public decimal Balance { get; set; }
}
