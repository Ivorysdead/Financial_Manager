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
        var accounts = _accountService.GetAccountsByUserIdAsync(userId);
        if (accounts == null || accounts.Count == 0)
        {
            return new NotFoundResult();
        }
        return new OkObjectResult(accounts);
    }
}

public class SwitchAccountFunction
{
    private readonly IAccountService _accountService;

    
    public SwitchAccountFunction(IAccountService accountService, ILogger<SwitchAccountFunction> logger)
    {
        _accountService = accountService;
    }
    
    [Function("SwitchAccount")]
    public async Task<IActionResult> SwitchAccount(
        [HttpTrigger(AuthorizationLevel.Function, "put", Route = "accounts/switch/{userId}/{accountId}")] HttpRequestData req,
        string userId,
        int accountId,
        ILogger log)
    {
        await _accountService.SwitchAccountAsync(userId, accountId);

        string message = $"Account switched to account with ID of {accountId} successfully!";
        return new OkObjectResult(new { Message = message});
    }
}