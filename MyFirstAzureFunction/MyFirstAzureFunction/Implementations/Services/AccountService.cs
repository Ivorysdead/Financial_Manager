using MyFirstAzureFunction.Interfaces;
using MyFirstAzureFunction.Models;

namespace MyFirstAzureFunction.Implementations.Services;

public class AccountService : IAccountService
{
    private readonly List<AccountModel> _accounts = new List<AccountModel>();  // "data store"

    public Task<List<AccountModel>> GetAccountsByUserIdAsync(string userId)
    {
        return Task.FromResult(_accounts.Where(a => a.UserID == userId).ToList());
    }

    public Task AddAccountAsync(AccountModel account)
    {
        _accounts.Add(account);
        return Task.CompletedTask;
    }

    public Task RemoveAccountAsync(int accountId)
    {
        var account = _accounts.FirstOrDefault(a => a.AccountId == accountId);
        if (account != null)
        {
            _accounts.Remove(account);
        }
        return Task.CompletedTask;
    }

    public Task SwitchAccountAsync(string userId, int accountId)
    {
        // Logic for switching accounts, e.g., update a 'CurrentAccountId' field for the user
        return Task.CompletedTask;
    }
}
