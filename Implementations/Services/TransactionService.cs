using Microsoft.Extensions.Logging;
using MyFirstAzureFunction.Interfaces;

namespace MyFirstAzureFunction.Services
{
    public class TransactionService : ITransactionCalculations
    {
        private readonly ILogger<TransactionService> _logger;

        // Constructor with logger injection
        public TransactionService(ILogger<TransactionService> logger)
        {
            _logger = logger; 
        }

        // Add transaction amount to current balance
        public double Add(double currentBalance, double transactionAmount)
        {
            _logger.LogInformation($"Adding {transactionAmount} to the current balance of {currentBalance}");
            double updatedBalance = currentBalance + transactionAmount;
            _logger.LogInformation($"Updated balance after addition: {updatedBalance}");
            return updatedBalance;
        }

        // Subtract transaction amount from current balance
        public double Subtract(double currentBalance, double transactionAmount)
        {
            _logger.LogInformation($"Subtracting {transactionAmount} from the current balance of {currentBalance}");
            double updatedBalance = currentBalance - transactionAmount;
            _logger.LogInformation($"Updated balance after subtraction: {updatedBalance}");
            return updatedBalance;
        }
    }
}