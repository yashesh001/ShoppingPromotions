using app.shoppingpromotions.infrastructure.Entities;

namespace app.shoppingpromotions.infrastructure.Interfaces
{
    public interface IDiscountPromotionRepository
    {
        Task<DiscountPromotion> GetActiveDiscountPromotionAsync(string productId, DateTime transactionDate);
    }
}
