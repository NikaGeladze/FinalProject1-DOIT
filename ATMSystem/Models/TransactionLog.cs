namespace ATMSystem.Models;

public class TransactionLog
{
    public string UserName { get; set; }
    public string Operation { get; set; }
    public decimal? Amount { get; set; }
    public decimal? CurrentBalance { get; set; }
    public DateTime Date { get; set; }
}