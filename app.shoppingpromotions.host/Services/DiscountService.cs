using app.shoppingpromotions.infrastructure.Interfaces;

namespace app.shoppingpromotions.host.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountPromotionRepository _discountPromotionRepository;

        public DiscountService(
            IDiscountPromotionRepository discountPromotionRepository)
        {
            _discountPromotionRepository = discountPromotionRepository;
        }

        public async Task<decimal> GetDiscountForProductAsync(string productId, DateTime transactionDate)
        {
            var currentDiscountPromotion = await _discountPromotionRepository.GetActiveDiscountPromotionAsync(productId, transactionDate);
            if (currentDiscountPromotion == null)
            {
                return 0.0m; // No active discount promotion
            }

            return currentDiscountPromotion.DiscountPercent;
        }
    }

}
