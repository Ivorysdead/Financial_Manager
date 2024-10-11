namespace MyFirstAzureFunction.Models;

public class TransactionModel
{
    public int TransactionId { get; set; }
    public int UserId { get; set; }
    public int LoanId { get; set; }
    public int AccountId { get; set; }
    public double TransactionAmount { get; set; }
    public double Balance { get; set; }
    public required string ArithmeticOperation { get; set; }
}