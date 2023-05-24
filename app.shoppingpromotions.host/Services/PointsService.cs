using app.shoppingpromotions.infrastructure.Interfaces;

namespace app.shoppingpromotions.host.Services
{
    public class PointsService : IPointsService
    {
        private readonly IPointsPromotionRepository _pointsPromotionRepository;

        public PointsService(IPointsPromotionRepository pointsPromotionRepository)
        {
            _pointsPromotionRepository = pointsPromotionRepository;
        }

        public async Task<int> GetPointsForProductAsync(string productCategory, DateTime transactionDate)
        {
            var currentPointsPromotion = await _pointsPromotionRepository.GetActivePointsPromotionAsync(productCategory, transactionDate);
            return (currentPointsPromotion == null) ? 0 // No active points promotion
                : (int)(currentPointsPromotion.PointsPerDollarSpent);
        }
    }
}
