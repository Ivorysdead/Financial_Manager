using MyFirstAzureFunction.Models;

namespace MyFirstAzureFunction.Interfaces;

public interface IAccountService
{
    Task<List<AccountModel>> GetAccountsByUserIdAsync(string userId);
    Task AddAccountAsync(AccountModel account);
    Task RemoveAccountAsync(int accountId);
    Task SwitchAccountAsync(string userId, int accountId);
}