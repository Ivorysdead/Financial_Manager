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

public class SwitchAccount
{
    private readonly IAccountService _accountService;

    public SwitchAccount(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    [Function("SwitchAccount")]
    public async Task<IActionResult> GetAccounts(
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