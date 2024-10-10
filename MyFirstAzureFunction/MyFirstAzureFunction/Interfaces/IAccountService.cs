using MyFirstAzureFunction.Models;

namespace MyFirstAzureFunction.Interfaces;

public interface IAccountService
{
    List<AccountModel> GetAccountsByUserIdAsync(string userId);
    Task SwitchAccountAsync(string userId, int accountId);
}