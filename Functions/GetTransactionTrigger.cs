using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using MyFirstAzureFunction.Models;
using Newtonsoft.Json;

namespace MyFirstAzureFunction.Functions
{
    public class GetTransactionTrigger
    {
        private readonly ILogger<GetTransactionTrigger> _logger;
        private readonly string FilePath = Path.Combine(Environment.CurrentDirectory, "transactions.txt");

        public GetTransactionTrigger(ILogger<GetTransactionTrigger> logger)
        {
            _logger = logger;
        }

        [Function("GetTransaction")]
        public async Task<HttpResponseData> GetTransactionAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "transaction/{transactionId}")] HttpRequestData req, string transactionId)
        {
            try
            {
                _logger.LogInformation($"Function Triggered: GetTransaction at {DateTime.Now}");
                
                // Validate and parse transactionId
                if (!int.TryParse(transactionId, out int parsedTransactionId))
                {
                    _logger.LogError($"Invalid transaction ID format: {transactionId}");
                    throw new ArgumentException($"Invalid transaction ID format: {transactionId}");
                }

                // Read all data from the .txt file to find desired transaction
                if (!File.Exists(FilePath))
                {
                    throw new FileNotFoundException($"Transaction file not found at path: {FilePath}");
                }

                var fileData = await File.ReadAllTextAsync(FilePath);
                var transactions = JsonConvert.DeserializeObject<List<TransactionModel>>(fileData);

                // Find the transaction by transactionId
                var transaction = transactions?.FirstOrDefault(t => t.TransactionId == parsedTransactionId);

                if (transaction == null)
                {
                    throw new ArgumentException($"Transaction with ID {parsedTransactionId} not found.");
                }

                // Create the response with the retrieved transaction based on transactionId
                var response = req.CreateResponse(HttpStatusCode.OK);
                response.Headers.Add("Content-Type", "application/json; charset=utf-8");
                await response.WriteStringAsync(JsonConvert.SerializeObject(transaction));

                _logger.LogInformation($"Transaction retrieved: {parsedTransactionId} at {DateTime.Now}");
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
}
