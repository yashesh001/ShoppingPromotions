using app.shoppingpromotions.infrastructure.Entities;

namespace app.shoppingpromotions.infrastructure.Interfaces
{
    public interface IPointsPromotionRepository
    {
        Task<PointsPromotion> GetActivePointsPromotionAsync(string productCategory, DateTime transactionDate);
    }
}
