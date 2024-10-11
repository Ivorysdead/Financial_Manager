using MyFirstAzureFunction.Models;

namespace MyFirstAzureFunction.Interfaces;

public interface IAccountService
{
    Task<List<AccountModel>> GetAccountsByUserIdAsync(string userId); // Change here
    Task SwitchAccountAsync(string userId, int accountId);
}