using MyFirstAzureFunction.Models;

namespace MyFirstAzureFunction.Interfaces
{
    public interface ITransactionCalculations
    {
        // Add a transaction amount to the balance
        double Add(double currentBalance, double transactionAmount);

        // Subtract a transaction amount from the balance
        double Subtract(double currentBalance, double transactionAmount);
    }
}