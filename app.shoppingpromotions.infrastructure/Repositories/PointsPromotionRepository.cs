using app.shoppingpromotions.infrastructure.Entities;
using app.shoppingpromotions.infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace app.shoppingpromotions.infrastructure.Repositories
{
    public class PointsPromotionRepository : IPointsPromotionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PointsPromotionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PointsPromotion> GetActivePointsPromotionAsync(string productCategory, DateTime transactionDate)
        {
            return await _dbContext.PointsPromotions
                .Where(pp => (pp.Category == "Any" || pp.Category == productCategory) && pp.StartDate <= transactionDate && pp.EndDate >= transactionDate)
                .OrderBy(pp => pp.Category != "Any")
                .ThenBy(pp => pp.StartDate)
                .FirstOrDefaultAsync();
        }
    }
}
