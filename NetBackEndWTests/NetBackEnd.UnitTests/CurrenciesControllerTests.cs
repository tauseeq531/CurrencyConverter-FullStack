using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using CurrencyConverter.Api.Controllers;
using CurrencyConverter.Api.Services;

namespace NetBackEnd.UnitTests
{
    [TestClass]
    public class CurrenciesControllerTests
    {
        [TestMethod]
        public void GetCurrencies_ReturnsOk_WithSortedList()
        {
            // Arrange
            var mockSvc = new Mock<IExchangeRateService>();
            mockSvc.Setup(s => s.GetKnownCurrencies()).Returns(new [] { "USD", "eur", "GBP" });
            var controller = new CurrenciesController(mockSvc.Object);

            // Act
            var action = controller.GetCurrencies();
            var result = action.Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var data = result.Value as IEnumerable<string>;
            Assert.IsNotNull(data);
            CollectionAssert.AreEqual(new List<string> { "eur", "GBP", "USD" }, data.ToList());
        }
    }
}