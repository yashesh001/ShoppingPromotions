using app.shoppingpromotions.infrastructure.Entities;
using app.shoppingpromotions.infrastructure.Interfaces;

namespace app.shoppingpromotions.infrastructure.Repositories
{
    public class StaticDiscountPromotionRepository : IDiscountPromotionRepository
    {
        private readonly List<DiscountPromotion> _discountPromotions;
        private readonly List<DiscountPromotionProduct> _discountPromotionProducts;

        public StaticDiscountPromotionRepository()
        {
            _discountPromotions = new List<DiscountPromotion>()
            {
                new DiscountPromotion()
                {
                    DiscountPromotionId = "DP001",
                    PromotionName = "Fuel Discount Promo",
                    StartDate = new DateTime(2020, 1, 1),
                    EndDate = new DateTime(2020, 2, 15),
                    DiscountPercent = 20
                },
                new DiscountPromotion()
                {
                    DiscountPromotionId = "DP002",
                    PromotionName = "Happy Promo",
                    StartDate = new DateTime(2020, 3, 2),
                    EndDate = new DateTime(2020, 3, 20),
                    DiscountPercent = 15
                }
            };

            _discountPromotionProducts = new List<DiscountPromotionProduct>()
            {
                new DiscountPromotionProduct()
                {
                    DiscountPromotionId = "DP001",
                    ProductId = "PRD02"
                },
                new DiscountPromotionProduct()
                {
                    DiscountPromotionId = "DP001",
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
