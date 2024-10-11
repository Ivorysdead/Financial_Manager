using MyFirstAzureFunction.Interfaces;
using MyFirstAzureFunction.Models;
using Newtonsoft.Json;

namespace MyFirstAzureFunction.Implementations.Services
{
    public class AccountService : IAccountService
    {
        private readonly string _filePath = "accounts.txt";

        // Reads all accounts from file
        public List<AccountModel> ReadAllAccounts()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    return new List<AccountModel>();
                }

                // Read the entire content of the file as a string
                var fileContent = File.ReadAllText(_filePath);
        
                // Deserialize the JSON array into a list of AccountModel objects
                var accounts = JsonConvert.DeserializeObject<List<AccountModel>>(fileContent);
                
                Console.WriteLine(Directory.GetCurrentDirectory());
        
                return accounts ?? new List<AccountModel>(); // Return an empty list if deserialization returns null
            }
            catch (Exception e)
            {
                throw new Exception("Error reading accounts from file", e);
            }
        }

        // Writes all accounts to the file
        private void WriteAllAccounts(List<AccountModel> accounts)
        {
            try
            {
                // Serialize the entire list as a single JSON array
                var serializedAccounts = JsonConvert.SerializeObject(accounts, Formatting.Indented);
        
                // Write the entire JSON array to the file
                File.WriteAllText(_filePath, serializedAccounts);
            }
            catch (Exception e)
            {
                throw new Exception("Error writing accounts to file", e);
            }
        }

        // Get all accounts for specified user
        public async Task<List<AccountModel>> GetAccountsByUserIdAsync(string userId) // Change here
        {
            try
            {
                var accounts = await Task.Run(() => ReadAllAccounts().Where(a => a.UserID == userId).ToList()); // Make it async
                return accounts;
            }
            catch (Exception e)
            {
                throw new Exception($"Error retrieving accounts for user ID: {userId}", e);
            }
        }

        // Switch between accounts
        public Task SwitchAccountAsync(string userId, int accountId)
        {
            try
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
            catch (Exception e)
            {
                throw new Exception($"Error switching account for user ID: {userId} and account ID: {accountId}", e);
            }
        }
    }
}
