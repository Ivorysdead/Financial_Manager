namespace MyFirstAzureFunction.Models;

public class AccountModel
{
    public int AccountId { get; set; } // Primary Key
    public int UserID { get; set; }  // Foreign key
    public int AccountType { get; set; }
    public string AccountName { get; set; }
    public double CurrentBalance { get; set; }
    
    // FIXME: make sure this doesn't interfere with Fabian's branch
    public bool IsActive { get; set; }
}