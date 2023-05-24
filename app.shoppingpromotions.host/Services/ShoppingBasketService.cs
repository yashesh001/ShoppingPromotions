using app.shoppingpromotions.domain;
using app.shoppingpromotions.infrastructure.Interfaces;

namespace app.shoppingpromotions.host.Services
{
    public class ShoppingBasketService : IShoppingBasketService
    {
        private readonly IProductRepository _productRepository;
        private readonly IDiscountService _discountService;
        private readonly IPointsService _pointsService;

        public ShoppingBasketService(IProductRepository productRepository, IDiscountService discountService, IPointsService pointsService)
        {
            _productRepository = productRepository;
            _discountService = discountService;
            _pointsService = pointsService;
        }

        public async Task<ShoppingResponse> ComputeShoppersBasketAsync(ShoppingRequest request)
        {
            decimal totalAmount = 0;
            decimal discountApplied = 0;
            int pointsEarned = 0;

            foreach (var item in request.Basket)
            {
                var product = await _productRepository.GetProductByIdAsync(item.ProductId);
                if (product == null)
                {
                    // Handle product not found error
                    return null;
                }

                var productTotalAmount = product.UnitPrice * item.Quantity;
                totalAmount += productTotalAmount;

                // Apply discount
                decimal itemDiscount = await _discountService.GetDiscountForProductAsync(item.ProductId, request.TransactionDate);
                discountApplied += itemDiscount * 0.01m * productTotalAmount;

                // Calculate points earned
                int itemPoints = await _pointsService.GetPointsForProductAsync(product.Category, request.TransactionDate);
                pointsEarned += (int)(itemPoints * productTotalAmount);
            }

            decimal grandTotal = totalAmount - discountApplied;

            var response = new ShoppingResponse
            {
                CustomerId = request.CustomerId,
                LoyaltyCard = request.LoyaltyCard,
                TransactionDate = request.TransactionDate,
                TotalAmount = totalAmount,
                DiscountApplied = discountApplied,
                GrandTotal = grandTotal,
                PointsEarned = pointsEarned
            };

            return response;
        }
    }
}
