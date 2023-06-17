using app.shoppingpromotions.infrastructure.Entities;
using app.shoppingpromotions.infrastructure.Interfaces;

namespace app.shoppingpromotions.host.Services
{
    public class ProductDiscountService : IProductDiscountService
    {
        private readonly IProductDiscountPromotionRepository _discountPromotionRepository;

        public ProductDiscountService(
            IProductDiscountPromotionRepository discountPromotionRepository)
        {
            _discountPromotionRepository = discountPromotionRepository;
        }

        public async Task<decimal> GetDiscountForProductAsync(Product product, DateTime transactionDate)
        {
            decimal itemDiscount = 0.0m;
            var currentDiscountPromotion = await _discountPromotionRepository.GetActiveDiscountPromotionAsync(product.ProductId, transactionDate);

            if (currentDiscountPromotion == null) // No active discount promotion
                return itemDiscount;

            switch (currentDiscountPromotion.DiscountPercent)
            {
                case > 0:
                    itemDiscount = currentDiscountPromotion.DiscountPercent * 0.01m * product.UnitPrice;
                    break;
                default:
                    if (currentDiscountPromotion.PriceDiscount > 0)
                    {
                        itemDiscount = currentDiscountPromotion.PriceDiscount;
                    }

                    break;
            }

            return itemDiscount;
        }
    }
}
