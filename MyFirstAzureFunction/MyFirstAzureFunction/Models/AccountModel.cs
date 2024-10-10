namespace MyFirstAzureFunction.Models;

public class AccountModel
{
    public int AccountId { get; set; } // Primary Key
    public string UserID { get; set; }  // Foreign key
    public string AccountType { get; set; }
    public string AccountName { get; set; }
    public decimal CurrentBalance { get; set; }
    
    // For account switch
    public bool IsActive { get; set; }
}