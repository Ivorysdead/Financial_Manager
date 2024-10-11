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
        try
        {
            var accounts = await _accountService.GetAccountsByUserIdAsync(userId); // What does await mean? It gives an error
            if (accounts == null || accounts.Count == 0)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(accounts);
        }
        catch (Exception e)
        {
            log.LogError(e, $"Error retrieving accounts for user ID: {userId}");
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }
}