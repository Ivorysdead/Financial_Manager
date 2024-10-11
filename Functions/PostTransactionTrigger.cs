using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using MyFirstAzureFunction.Models;
using MyFirstAzureFunction.Services;
using Newtonsoft.Json;

namespace MyFirstAzureFunction.Functions;

public class PostTransactionTrigger
{
    private readonly ILogger _logger;
    private readonly ILoggerFactory _loggerFactory;
    private readonly string FilePath = Path.Combine(Environment.CurrentDirectory, "transactions.txt");

    public PostTransactionTrigger(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
        _logger = _loggerFactory.CreateLogger<PostTransactionTrigger>();
    }

    [Function("PostTransaction")]
    public async Task<HttpResponseData> PostTransactionAsync([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
    {
        try
        {
            _logger.LogInformation($"Function Triggered: PostTransaction at {DateTime.Now}");

            // Read the request body and break it down into the TransactionModel format/pattern
            var requestBody = await req.ReadAsStringAsync();
            var newTransaction = JsonConvert.DeserializeObject<TransactionModel>(requestBody);

            // Make sure transaction data is valid
            if (newTransaction == null || newTransaction.TransactionAmount <= 0 || string.IsNullOrEmpty(newTransaction.ArithmeticOperation))
            {
                _logger.LogWarning("Invalid transaction data");
                var errorResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await errorResponse.WriteStringAsync("Invalid transaction data in request.");
                return errorResponse;
            }

            // Read transactions from the file
            _logger.LogInformation($"Reading transactions from file: {FilePath}");
            List<TransactionModel> transactions = new List<TransactionModel>();

            // Make sure the file exists and is accessible
            if (File.Exists(FilePath))
            {
                var fileData = await File.ReadAllTextAsync(FilePath);
                _logger.LogInformation($"File Content: {fileData}");

                transactions = JsonConvert.DeserializeObject<List<TransactionModel>>(fileData) ?? new List<TransactionModel>();
            }
            else
            {
                _logger.LogError($"Transaction file not found: {FilePath}");
                var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
                await errorResponse.WriteStringAsync("Transaction file not found.");
                return errorResponse;
            }

            // Use TransactionService to calculate the new balance after the transaction
            var transactionLogger = _loggerFactory.CreateLogger<TransactionService>();
            var transactionService = new TransactionService(transactionLogger);

            if (newTransaction.ArithmeticOperation == "Add")
            {
                newTransaction.Balance = transactionService.Add(newTransaction.Balance, newTransaction.TransactionAmount);
            }
            else if (newTransaction.ArithmeticOperation == "Subtract")
            {
                newTransaction.Balance = transactionService.Subtract(newTransaction.Balance, newTransaction.TransactionAmount);
            }
            else
            {
                var errorResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await errorResponse.WriteStringAsync("Invalid ArithmeticOperation.");
                return errorResponse;
            }

            // Add the new transaction to the list
            transactions.Add(newTransaction);

            // Update the list back to the file
            await File.WriteAllTextAsync(FilePath, JsonConvert.SerializeObject(transactions));

            // Create a success response
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");
            await response.WriteStringAsync("Transaction successfully created.");

            _logger.LogInformation($"Transaction created successfully at {DateTime.Now}");
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}");
            var response = req.CreateResponse(HttpStatusCode.BadRequest);
            await response.WriteStringAsync($"Error: {ex.Message}");
            return response;
        }
    }
}
