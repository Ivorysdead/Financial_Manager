using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using MyFirstAzureFunction.Functions;
using MyFirstAzureFunction.Interfaces;
using MyFirstAzureFunction.Models;
using Newtonsoft.Json;

namespace MyFirstAzureFunction.Tests.Functions;

public class SwitchAccountTests
{
    /*
    private readonly Mock<ILogger<QuickTrigger>> _logger;
    private readonly Mock<IQuickCalculations> _quickCalculations;
    private readonly QuickTrigger _sut; //SUT = System Under Test

    public SwitchAccountTests()
    {


        _logger = new Mock<ILogger<QuickTrigger>>();
        _quickCalculations = new Mock<IQuickCalculations>();
        _sut = new QuickTrigger(_logger.Object, _quickCalculations.Object);
    }

     [Test]
    public async Task Given_ValidUserIdAndAccountId_Then_SwitchAccount_Returns_SuccessMessage()
    {
        // Arrange
        var userId = "jimmy";
        var accountId = 101;
        var expectedMessage = $"Account switched to account with ID of {accountId} successfully!";

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ILoggerFactory, LoggerFactory>();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var context = new Mock<FunctionContext>();
        context.SetupProperty(c => c.InstanceServices, serviceProvider);
        var request = new Mock<HttpRequestData>(context.Object);

        var logger = new Mock<ILogger<SwitchAccountFunction>>(); // Reference to SwitchAccountFunction logger
        var accountService = new Mock<IAccountService>();
        accountService.Setup(s => s.SwitchAccountAsync(userId, accountId)).Returns(Task.CompletedTask);

        var sut = new SwitchAccountFunction(accountService.Object, logger.Object); // Instantiate SwitchAccountFunction class

        // Act
        var result = await sut.SwitchAccount(request.Object, userId, accountId, logger.Object); // Call SwitchAccount method

        // Assert
        Assert.That(result, Is.TypeOf<OkObjectResult>()); // Check if the result is OkObjectResult
        var okResult = (OkObjectResult)result;
        var expectedResponse = new { Message = expectedMessage };
        Assert.That(okResult.Value, Is.EqualTo(expectedResponse)); // Compare objects directly
    }



    [Test]
    public async Task Given_InvalidAccountId_Then_SwitchAccount_Returns_NotFound()
    {
        // Arrange
        var userId = "jimmy";
        var invalidAccountId = 999; // An account ID that doesn't exist for the user

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ILoggerFactory, LoggerFactory>();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var context = new Mock<FunctionContext>();
        context.SetupProperty(c => c.InstanceServices, serviceProvider);
        var request = new Mock<HttpRequestData>(context.Object);

        var logger = new Mock<ILogger<SwitchAccountFunction>>();
        var accountService = new Mock<IAccountService>();
        accountService.Setup(s => s.SwitchAccountAsync(userId, invalidAccountId)).Throws(new KeyNotFoundException());

        var sut = new SwitchAccountFunction(accountService.Object, logger.Object);

        // Act
        var result = await sut.SwitchAccount(request.Object, userId, invalidAccountId, logger.Object);

        // Assert
        Assert.That(result, Is.TypeOf<NotFoundResult>());
    }

    [Test]
    public async Task Given_NoAccountsForUser_Then_SwitchAccount_Returns_NotFound()
    {
        // Arrange
        var userId = "newUser";
        var accountId = 101;

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ILoggerFactory, LoggerFactory>();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var context = new Mock<FunctionContext>();
        context.SetupProperty(c => c.InstanceServices, serviceProvider);
        var request = new Mock<HttpRequestData>(context.Object);

        var logger = new Mock<ILogger<SwitchAccountFunction>>();
        var accountService = new Mock<IAccountService>();
        accountService.Setup(s => s.SwitchAccountAsync(userId, accountId)).Throws(new Exception("No accounts found"));

        var sut = new SwitchAccountFunction(accountService.Object, logger.Object);

        // Act
        var result = await sut.SwitchAccount(request.Object, userId, accountId, logger.Object);

        // Assert
        Assert.That(result, Is.TypeOf<NotFoundResult>());
    }

    [Test]
    public async Task Given_ErrorOccurs_Then_SwitchAccount_LogsErrorAndReturns_InternalServerError()
    {
        // Arrange
        var userId = "jimmy";
        var accountId = 101;

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ILoggerFactory, LoggerFactory>();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var context = new Mock<FunctionContext>();
        context.SetupProperty(c => c.InstanceServices, serviceProvider);
        var request = new Mock<HttpRequestData>(context.Object);

        var logger = new Mock<ILogger<SwitchAccountFunction>>();
        var accountService = new Mock<IAccountService>();
        accountService.Setup(s => s.SwitchAccountAsync(userId, accountId)).Throws(new Exception("Some error occurred"));

        var sut = new SwitchAccountFunction(accountService.Object, logger.Object);

        // Act
        var result = await sut.SwitchAccount(request.Object, userId, accountId, logger.Object);

        // Assert
        Assert.That(result, Is.TypeOf<ObjectResult>());
        var objectResult = (ObjectResult)result;
        Assert.That(objectResult.StatusCode, Is.EqualTo((int)HttpStatusCode.InternalServerError));

        logger.Verify(log => log.Log(
            LogLevel.Error,
            It.IsAny<EventId>(),
            It.IsAny<It.IsAnyType>(),
            It.IsAny<Exception>(),
            (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Once);
    }
     */
    

}