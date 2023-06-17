using app.shoppingpromotions.infrastructure.Entities;
using app.shoppingpromotions.infrastructure.Interfaces;

namespace app.shoppingpromotions.infrastructure.Repositories
{
    public class StaticProductDiscountPromotionRepository : IProductDiscountPromotionRepository
    {
        private readonly List<DiscountPromotion> _discountPromotions;
        private readonly List<DiscountPromotionProduct> _discountPromotionProducts;

        public StaticProductDiscountPromotionRepository()
        {
            _discountPromotions = new List<DiscountPromotion>()
            {
                new DiscountPromotion()
                {
                    DiscountPromotionId = "DP001",
                    PromotionName = "Current month special on VB",
                    StartDate = new DateTime(2023, 6, 1),
                    EndDate = new DateTime(2023, 6, 30),
                    PriceDiscount = 2
                },
                new DiscountPromotion()
                {
                    DiscountPromotionId = "DP002",
                    PromotionName = "Special 10% off on Crown Lager",
                    StartDate = new DateTime(2023, 6, 10),
                    EndDate = new DateTime(2023, 6, 20),
                    DiscountPercent = 5
                }
            };

            _discountPromotionProducts = new List<DiscountPromotionProduct>()
            {
                new DiscountPromotionProduct()
                {
                    DiscountPromotionId = "DP001",
                    ProductId = "PRD01"
                },
                new DiscountPromotionProduct()
                {
                    DiscountPromotionId = "DP002",
                    ProductId = "PRD02"
                }
            };
        }

        public async Task<DiscountPromotion> GetActiveDiscountPromotionAsync(string productId, DateTime transactionDate)
        {
            return await Task.FromResult(
                (from dp in _discountPromotions
                 join dpp in _discountPromotionProducts on dp.DiscountPromotionId equals dpp.DiscountPromotionId
                 where dp.StartDate <= transactionDate && dp.EndDate >= transactionDate && string.Equals(dpp.ProductId, productId)
                 select dp).FirstOrDefault()
            );
        }
    }
}
