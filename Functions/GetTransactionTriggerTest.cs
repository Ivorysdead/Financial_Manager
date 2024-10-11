using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using MyFirstAzureFunction.Functions;
using MyFirstAzureFunction.Models;
using Newtonsoft.Json;

namespace MyFirstAzureFunction.Tests.Functions
{
    public class GetTransactionTriggerTest
    {
        private GetTransactionTrigger _sut; // System under test
        private const string FilePath = "transactions.txt";

        [SetUp]
        public void Setup()
        {
            var loggerMock = new Mock<ILogger<GetTransactionTrigger>>();
            _sut = new GetTransactionTrigger(loggerMock.Object);
        }

        [Test]
        public async Task Given_InvalidTransactionId_Then_Returns_BadRequest()
        {
            var invalidTransactionId = "999";
            var serviceCollection = new ServiceCollection();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var context = new Mock<FunctionContext>();
            context.SetupProperty(c => c.InstanceServices, serviceProvider);
            var request = new Mock<HttpRequestData>(context.Object);

            request.Setup(req => req.CreateResponse()).Returns(() =>
            {
                var response = new Mock<HttpResponseData>(context.Object);
                response.SetupProperty(r => r.Headers, new HttpHeadersCollection());
                response.SetupProperty(r => r.StatusCode, HttpStatusCode.OK);
                response.SetupProperty(r => r.Body, new MemoryStream());
                return response.Object;
            });
            var result = await _sut.GetTransactionAsync(request.Object, invalidTransactionId);
            
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Given_ValidTransactionId_Then_Returns_Transaction()
        {
            var transactionId = "1";
            var expectedTransaction = new TransactionModel
            {
                TransactionId = 1,
                UserId = 123,
                LoanId = 456,
                AccountId = 789,
                TransactionAmount = 100,
                Balance = 1000,
                ArithmeticOperation = "Add"
            };

            // HTTPRequestData Setup
            var serviceCollection = new ServiceCollection();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var context = new Mock<FunctionContext>();
            context.SetupProperty(c => c.InstanceServices, serviceProvider);
            var request = new Mock<HttpRequestData>(context.Object);

            request.Setup(req => req.CreateResponse()).Returns(() =>
            {
                var response = new Mock<HttpResponseData>(context.Object);
                response.SetupProperty(r => r.Headers, new HttpHeadersCollection());
                response.SetupProperty(r => r.StatusCode, HttpStatusCode.OK);
                response.SetupProperty(r => r.Body, new MemoryStream());
                return response.Object;
            });

            // reading from the file
            var fileData = JsonConvert.SerializeObject(new List<TransactionModel> { expectedTransaction });
            File.WriteAllText(FilePath, fileData);
            
            var result = await _sut.GetTransactionAsync(request.Object, transactionId);
            
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var responseContent = await new StreamReader(result.Body).ReadToEndAsync();
            var actualTransaction = JsonConvert.DeserializeObject<TransactionModel>(responseContent);
            if (actualTransaction != null)
                Assert.That(actualTransaction.TransactionId, Is.EqualTo(expectedTransaction.TransactionId));
        }
    }
}
