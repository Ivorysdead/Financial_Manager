using MyFirstAzureFunction.Interfaces;
using MyFirstAzureFunction.Models;

namespace MyFirstAzureFunction.Implementations.Services;

public class AccountService : IAccountService
{
    
    // "data store"
    private readonly List<AccountModel> _accounts = new List<AccountModel>();

    
    // Get Accounts
    public Task<List<AccountModel>> GetAccountsByUserIdAsync(string userId)
    {
        return Task.FromResult(_accounts.Where(a => a.UserID == userId).ToList());
    }

    
    // Add new Account
    public Task AddAccountAsync(AccountModel account)
    {
        _accounts.Add(account);
        return Task.CompletedTask;
    }

    
    // Remove Account
    public Task RemoveAccountAsync(int accountId)
    {
        var account = _accounts.FirstOrDefault(a => a.AccountId == accountId);
        if (account != null)
        {
            _accounts.Remove(account);
        }
        return Task.CompletedTask;
    }

    
    // Switch Between Accounts
    public Task SwitchAccountAsync(string userId, int accountId)
    {
        
        // Update a 'CurrentAccountId' field for the user
        return Task.CompletedTask;
    }
}
