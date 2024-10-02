namespace MyFirstAzureFunction.Models;

public class AccountModel
{
    public int AccountId { get; set; } // Primary Key
    public string AccountType { get; set; }
    public string AccountName { get; set; }
    public decimal CurrentBalance { get; set; }
    public string UserID { get; set; }  // Foreign key
}