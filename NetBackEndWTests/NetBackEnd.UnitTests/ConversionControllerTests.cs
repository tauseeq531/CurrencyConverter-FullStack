using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using CurrencyConverter.Api.Controllers;
using CurrencyConverter.Api.Services;
using CurrencyConverter.Api.Repositories;
using CurrencyConverter.Api.Dtos;
using CurrencyConverter.Api.Models;

namespace NetBackEnd.UnitTests
{
    [TestClass]
    public class ConversionControllerTests
    {
        private Fixture _fixture = null!;
        private Mock<IConversionService> _service = null!;
        private Mock<IConversionRepository> _repo = null!;
        private Mock<IMapper> _mapper = null!;
        private ConversionController _controller = null!;

        [TestInitialize]
        public void SetUp()
        {
            _fixture = new Fixture();
            _service = new Mock<IConversionService>();
            _repo = new Mock<IConversionRepository>();
            _mapper = new Mock<IMapper>();
            _controller = new ConversionController(_service.Object, _repo.Object, _mapper.Object);
        }

        [TestMethod]
        public async Task Convert_ReturnsOk_WhenValidRequest()
        {
            // Arrange
            var request = _fixture.Build<ConvertRequest>()
                                  .With(x => x.FromCurrency, "USD")
                                  .With(x => x.ToCurrency, "EUR")
                                  .With(x => x.Amount, 100m)
                                  .Create();

            decimal converted = 92.5m;
            decimal rate = 0.925m;
            var ts = System.DateTime.UtcNow;

            _service.Setup(s => s.ConvertAsync(request.Amount, request.FromCurrency, request.ToCurrency, It.IsAny<CancellationToken>()))
                    .ReturnsAsync((converted, rate, ts));

            Conversion capturedEntity = null!;
            _repo.Setup(r => r.AddAsync(It.IsAny<Conversion>(), It.IsAny<CancellationToken>()))
                 .Callback<Conversion, CancellationToken>((e, _) => capturedEntity = e)
                 .Returns(Task.CompletedTask);

            _repo.Setup(r => r.SaveAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            _mapper.Setup(m => m.Map<ConvertResponse>(It.IsAny<Conversion>()))
                   .Returns(() => new ConvertResponse
                   {
                       ConvertedAmount = capturedEntity?.ToAmount ?? converted,
                       Rate = capturedEntity?.RateUsed ?? rate,
                       FromCurrency = capturedEntity?.FromCurrency ?? request.FromCurrency,
                       ToCurrency = capturedEntity?.ToCurrency ?? request.ToCurrency,
                       TimeUtc = ts
                   });

            // Act
            var action = await _controller.Convert(request, CancellationToken.None);
            var result = action.Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var payload = result.Value as ConvertResponse;
            Assert.IsNotNull(payload);
            Assert.AreEqual(converted, payload.ConvertedAmount);
            Assert.AreEqual(rate, payload.Rate);
            Assert.AreEqual("USD", payload.FromCurrency);
            Assert.AreEqual("EUR", payload.ToCurrency);
        }
    }
}