using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;
using CurrencyConverter.Api.Controllers;
using CurrencyConverter.Api.Services;

namespace NetBackEnd.UnitTests
{
    [TestClass]
    public class RatesControllerTests
    {
        [TestMethod]
        public async Task GetRates_ReturnsOk_WithPayload()
        {
            // Arrange
            var mockSvc = new Mock<IExchangeRateService>();
            var when = DateTime.UtcNow;
            var rates = new Dictionary<string, decimal> { ["USD"] = 1m, ["EUR"] = 0.92m };
            mockSvc.Setup(s => s.GetLatestRatesAsync("USD", It.IsAny<CancellationToken>()))
                   .ReturnsAsync((when, rates));

            var controller = new RatesController(mockSvc.Object);

            // Act
            var action = await controller.GetRates("USD", CancellationToken.None);
            var result = action.Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result.Value);
        }
    }
}