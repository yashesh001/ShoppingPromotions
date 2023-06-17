using app.shoppingpromotions.infrastructure.Entities;

namespace app.shoppingpromotions.infrastructure.Interfaces
{
    public interface IProductDiscountPromotionRepository
    {
        Task<DiscountPromotion> GetActiveDiscountPromotionAsync(string productId, DateTime transactionDate);
    }
}
