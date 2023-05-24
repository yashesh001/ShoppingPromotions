using app.shoppingpromotions.infrastructure.Entities;
using app.shoppingpromotions.infrastructure.Interfaces;

namespace app.shoppingpromotions.infrastructure.Repositories
{
    public class StaticPointsPromotionRepository : IPointsPromotionRepository
    {
        private readonly List<PointsPromotion> _pointsPromotions;

        public StaticPointsPromotionRepository()
        {
            _pointsPromotions = new List<PointsPromotion>()
            {
                new PointsPromotion()
                {
                    PointsPromotionId = "PP001",
                    PromotionName = "New Year Promo",
                    StartDate = new DateTime(2020, 1, 1),
                    EndDate = new DateTime(2020, 1, 30),
                    Category = "Any",
                    PointsPerDollarSpent = 2
                },
                new PointsPromotion()
                {
                    PointsPromotionId = "PP002",
                    PromotionName = "Fuel Promo",
                    StartDate = new DateTime(2020, 2, 5),
                    EndDate = new DateTime(2020, 2, 15),
                    Category = "Fuel",
                    PointsPerDollarSpent = 3
                },
                new PointsPromotion()
                {
                    PointsPromotionId = "PP003",
                    PromotionName = "Shop Promo",
                    StartDate = new DateTime(2020, 3, 1),
                    EndDate = new DateTime(2020, 3, 20),
                    Category = "Shop",
                    PointsPerDollarSpent = 4
                }
            };
        }

        public async Task<PointsPromotion> GetActivePointsPromotionAsync(string productCategory, DateTime transactionDate)
        {
            return await Task.FromResult(_pointsPromotions
                .Where(pp => (pp.Category == "Any" || pp.Category == productCategory) && pp.StartDate <= transactionDate && pp.EndDate >= transactionDate)
                .OrderBy(pp => pp.Category != "Any")
                .ThenBy(pp => pp.StartDate)
                .FirstOrDefault());
        }
    }
}
