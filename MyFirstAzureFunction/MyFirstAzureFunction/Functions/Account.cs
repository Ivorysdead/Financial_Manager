using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker.Http;
using MyFirstAzureFunction.Implementations.Services;
using MyFirstAzureFunction.Interfaces;
using System.Threading.Tasks;
using MyFirstAzureFunction.Models;
using Newtonsoft.Json;

namespace MyFirstAzureFunction.Functions;

public class Account
{
    private readonly IAccountService _accountService;

    public Account(IAccountService accountService)
    {
        _accountService = accountService;
    }

    // Get all accounts for a user
    [Function("GetAccounts")]
    public async Task<IActionResult> GetAccounts(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "accounts/{userId}")] HttpRequest req,
        string userId,
        ILogger log)
    {
        var accounts = await _accountService.GetAccountsByUserIdAsync(userId);
        if (accounts == null || accounts.Count == 0)
        {
            return new NotFoundResult();
        }
        return new OkObjectResult(accounts);
    }

    // Add new account
    [Function("AddAccount")]
    public async Task<IActionResult> AddAccount(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "accounts")] AccountModel account,
        ILogger log)
    {
        await _accountService.AddAccountAsync(account);
        return new OkResult();
    }

    // Remove account
    [Function("RemoveAccount")]
    public async Task<IActionResult> RemoveAccount(
        [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "accounts/{accountId}")] HttpRequest req,
        int accountId,
        ILogger log)
    {
        await _accountService.RemoveAccountAsync(accountId);
        return new OkResult();
    }

    // Switch account
    [Function("SwitchAccount")]
    public async Task<IActionResult> SwitchAccount(
        [HttpTrigger(AuthorizationLevel.Function, "put", Route = "accounts/switch/{userId}/{accountId}")] HttpRequest req,
        string userId,
        int accountId,
        ILogger log)
    {
        await _accountService.SwitchAccountAsync(userId, accountId);
        return new OkResult();
    }
}