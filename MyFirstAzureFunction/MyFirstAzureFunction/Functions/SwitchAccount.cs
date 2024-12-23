using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
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
    public async Task<IActionResult> GetAccount(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "accounts/switch")] HttpRequestData req,
        ILogger log)
    {
        try
        {
            // Read and deserialize the request body
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            SwitchAccountRequest requestData;

            try
            {
                requestData = JsonConvert.DeserializeObject<SwitchAccountRequest>(requestBody);
            }
            catch (JsonException jsonEx)
            {
                log.LogError(jsonEx, "Error deserializing request body.");
                return new BadRequestObjectResult("Invalid JSON format.");
            }

            if (requestData == null || string.IsNullOrEmpty(requestData.UserId) || requestData.AccountId <= 0)
            {
                return new BadRequestObjectResult("Invalid request data.");
            }

            // Call the service to switch the account
            await _accountService.SwitchAccountAsync(requestData.UserId, requestData.AccountId);

            string message = $"Account switched to account with ID of {requestData.AccountId} successfully!";
            return new OkObjectResult(new { Message = message });
        }
        catch (Exception e)
        {
            log.LogError(e, "An error occurred while switching accounts.");
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }
}