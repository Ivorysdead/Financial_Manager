using MyFirstAzureFunction.Interfaces;
using MyFirstAzureFunction.Models;
using Newtonsoft.Json;

namespace MyFirstAzureFunction.Implementations.Services
{
    public class AccountService : IAccountService
    {
        private readonly string _filePath = "accounts.txt";

        // Reads all accounts from file
        private List<AccountModel> ReadAllAccounts()
        {
            try
            {
                if (!File.Exists(_filePath)) return new List<AccountModel>();

                var accounts = File.ReadAllLines(_filePath)
                                   .Select(JsonConvert.DeserializeObject<AccountModel>)
                                   .Where(account => account != null)
                                   .ToList();
                
                // Log the number of accounts read
                Console.WriteLine($"Read {accounts.Count} accounts from file.");
                return accounts;
            }
            catch (Exception e)
            {
                throw new Exception("Error reading accounts in file", e);
            }
        }

        
        // Writes all accounts to the file
        private void WriteAllAccounts(List<AccountModel> accounts)
        {
            try
            {
                var serializedAccounts = accounts.Select(JsonConvert.SerializeObject);
                File.WriteAllLines(_filePath, serializedAccounts);
            }
            catch (Exception e)
            {
                throw new Exception("Error writing accounts to file", e);
            }
        }

        
        // Get all accounts for specified user
        public Task<List<AccountModel>> GetAccountsByUserIdAsync(string userId)
        {
            var accounts = ReadAllAccounts().Where(a => a.UserID == userId).ToList();
            return Task.FromResult(accounts);
        }

        
        // Add new account
        public Task AddAccountAsync(AccountModel account)
        {
            var accounts = ReadAllAccounts();
            accounts.Add(account); // Adds new account
            WriteAllAccounts(accounts); // Updates file list

            return Task.CompletedTask;
        }

        
        // Remove an account
        public Task RemoveAccountAsync(int accountId)
        {
            var accounts = ReadAllAccounts();
            var account = accounts.FirstOrDefault(a => a.AccountId == accountId);

            if (account != null)
            {
                accounts.Remove(account); // Removes account
                WriteAllAccounts(accounts); // Updates file list
            }

            return Task.CompletedTask;
        }

        
        // Switch between accounts
        public Task SwitchAccountAsync(string userId, int accountId)
        {
            var accounts = ReadAllAccounts();
            var accountToSwitch = accounts.FirstOrDefault(a => a.AccountId == accountId && a.UserID == userId);

            if (accountToSwitch != null)
            {
                // Business logic to mark the account as active (switch)
                foreach (var account in accounts.Where(a => a.UserID == userId))
                {
                    account.IsActive = false; // All other accounts inactive
                }
                accountToSwitch.IsActive = true; // Switch to the desired account

                WriteAllAccounts(accounts); // Update the file with new state
            }

            return Task.CompletedTask;
        }
    }
}
