using app.shoppingpromotions.domain;
using app.shoppingpromotions.host.Services;
using app.shoppingpromotions.infrastructure.Entities;
using app.shoppingpromotions.infrastructure.Interfaces;
using Moq;

namespace app.shoppingpromotions.test
{
    public class ShoppingBasketServiceTest
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IDiscountService> _discountServiceMock;
        private readonly Mock<IPointsService> _pointsServiceMock;
        private readonly ShoppingBasketService _shoppingBasketService;

        public ShoppingBasketServiceTest()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _discountServiceMock = new Mock<IDiscountService>();
            _pointsServiceMock = new Mock<IPointsService>();
            _shoppingBasketService = new ShoppingBasketService(_productRepositoryMock.Object, _discountServiceMock.Object, _pointsServiceMock.Object);
        }

        [Theory]
        [InlineData("2020-1-2", "20", "7.3", "0.52", "6.78", 12)]
        [InlineData("2020-2-10", "0", "7.3", "0.00", "7.3", 17)]
        [InlineData("2020-3-6", "15", "7.3", "0.39", "6.91", 0)]
        public async Task ComputeShoppersBasketAsync_ShouldCalculateTotalsCorrectly(
            string transactionDate, 
            string discountPercentage,
            string totalAmount,
            string discountApplied, 
            string grandTotal,
            int pointsEarned)
        {
            // Arrange
            var request = new ShoppingRequest
            {
                CustomerId = "8e4e8991-aaee-495b-9f24-52d5d0e509c5",
                LoyaltyCard = "CTX0000001",
                TransactionDate = DateTime.Parse(transactionDate),
                Basket = new List<BasketItem>
                {
                    new BasketItem { ProductId = "PRD01", UnitPrice = 1.2m, Quantity = 3 },
                    new BasketItem { ProductId = "PRD02", UnitPrice = 2.0m, Quantity = 2 },
                    new BasketItem { ProductId = "PRD03", UnitPrice = 5.0m, Quantity = 1 }
                }
            };

            _productRepositoryMock.Setup(repo => repo.GetProductByIdAsync("PRD01"))
                .ReturnsAsync(new Product { ProductId = "PRD01", Category = "Fuel", UnitPrice = 1.2m });
            _productRepositoryMock.Setup(repo => repo.GetProductByIdAsync("PRD02"))
                .ReturnsAsync(new Product { ProductId = "PRD02", Category = "Fuel", UnitPrice = 1.3m });
            _productRepositoryMock.Setup(repo => repo.GetProductByIdAsync("PRD03"))
                .ReturnsAsync(new Product { ProductId = "PRD03", Category = "Shop", UnitPrice = 1.1m });

            _discountServiceMock.Setup(service => service.GetDiscountForProductAsync("PRD01", request.TransactionDate))
                .ReturnsAsync(0.0m);
            _discountServiceMock.Setup(service => service.GetDiscountForProductAsync("PRD02", request.TransactionDate))
                .ReturnsAsync(Decimal.Parse(discountPercentage));
            _discountServiceMock.Setup(service => service.GetDiscountForProductAsync("PRD03", request.TransactionDate))
                .ReturnsAsync(0.0m);

            _pointsServiceMock.Setup(service => service.GetPointsForProductAsync("Fuel", new DateTime(2020, 1, 2)))
                .ReturnsAsync(2);
            _pointsServiceMock.Setup(service => service.GetPointsForProductAsync("Fuel", new DateTime(2020, 2, 10)))
                .ReturnsAsync(3);
            _pointsServiceMock.Setup(service => service.GetPointsForProductAsync("Shop", new DateTime(2020, 3, 5)))
                .ReturnsAsync(4);

            // Act
            var result = await _shoppingBasketService.ComputeShoppersBasketAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("8e4e8991-aaee-495b-9f24-52d5d0e509c5", result.CustomerId);
            Assert.Equal("CTX0000001", result.LoyaltyCard);
            Assert.Equal(DateTime.Parse(transactionDate), result.TransactionDate);
            Assert.Equal(Decimal.Parse(totalAmount), result.TotalAmount);
            Assert.Equal(Decimal.Parse(discountApplied), result.DiscountApplied);
            Assert.Equal(Decimal.Parse(grandTotal), result.GrandTotal);
            Assert.Equal(pointsEarned, result.PointsEarned);
        }
    }
}