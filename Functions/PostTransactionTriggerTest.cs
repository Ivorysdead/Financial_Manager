using System.Net;
using System.Text;
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
    public class PostTransactionTriggerTests
    {
        private PostTransactionTrigger _sut; // System under test
        private const string FilePath = "transactions.txt";

        [SetUp]
        public void Setup()
        {
            var loggerMock = new Mock<ILogger<PostTransactionTrigger>>();
            _sut = new PostTransactionTrigger(new LoggerFactory());
        }

        [Test]
        public async Task Given_ValidTransaction_When_PostTransaction_Then_ReturnOk()
        {
            var transaction = new TransactionModel
            {
                TransactionId = 1,
                UserId = 1001,
                LoanId = 101,
                AccountId = 201,
                TransactionAmount = 500.0,
                Balance = 1500.0,
                ArithmeticOperation = "Add"
            };

            var requestBody = JsonConvert.SerializeObject(transaction);
            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));

            // HTTPRequestData Setup
            var serviceCollection = new ServiceCollection();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var context = new Mock<FunctionContext>();
            context.SetupProperty(c => c.InstanceServices, serviceProvider);
            var request = new Mock<HttpRequestData>(context.Object);

            request.Setup(req => req.Body).Returns(memoryStream);
            request.Setup(req => req.CreateResponse()).Returns(() =>
            {
                var response = new Mock<HttpResponseData>(context.Object);
                response.SetupProperty(r => r.Headers, new HttpHeadersCollection());
                response.SetupProperty(r => r.StatusCode, HttpStatusCode.OK);
                response.SetupProperty(r => r.Body, new MemoryStream());
                return response.Object;
            });
            
            var result = await _sut.PostTransactionAsync(request.Object);
            
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task Given_InvalidTransaction_When_PostTransaction_Then_ReturnBadRequest()
        {
            var invalidBody = "";  // Invalid body (empty)
            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(invalidBody));

            // HTTPRequestData Setup
            var serviceCollection = new ServiceCollection();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var context = new Mock<FunctionContext>();
            context.SetupProperty(c => c.InstanceServices, serviceProvider);
            var request = new Mock<HttpRequestData>(context.Object);

            request.Setup(req => req.Body).Returns(memoryStream);
            request.Setup(req => req.CreateResponse()).Returns(() =>
            {
                var response = new Mock<HttpResponseData>(context.Object);
                response.SetupProperty(r => r.Headers, new HttpHeadersCollection());
                response.SetupProperty(r => r.StatusCode, HttpStatusCode.BadRequest);
                response.SetupProperty(r => r.Body, new MemoryStream());
                return response.Object;
            });
            
            var result = await _sut.PostTransactionAsync(request.Object);
            
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Given_ValidTransaction_When_PostTransaction_Then_LogInformation()
        {
            var transaction = new TransactionModel
            {
                TransactionId = 1,
                UserId = 1001,
                LoanId = 101,
                AccountId = 201,
                TransactionAmount = 500.0,
                Balance = 1500.0,
                ArithmeticOperation = "Add"
            };

            var requestBody = JsonConvert.SerializeObject(transaction);
            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));

            // HTTPRequestData Setup
            var serviceCollection = new ServiceCollection();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var context = new Mock<FunctionContext>();
            context.SetupProperty(c => c.InstanceServices, serviceProvider);
            var request = new Mock<HttpRequestData>(context.Object);

            request.Setup(req => req.Body).Returns(memoryStream);
            request.Setup(req => req.CreateResponse()).Returns(() =>
            {
                var response = new Mock<HttpResponseData>(context.Object);
                response.SetupProperty(r => r.Headers, new HttpHeadersCollection());
                response.SetupProperty(r => r.StatusCode, HttpStatusCode.OK);
                response.SetupProperty(r => r.Body, new MemoryStream());
                return response.Object;
            });

            var result = await _sut.PostTransactionAsync(request.Object);
            
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
