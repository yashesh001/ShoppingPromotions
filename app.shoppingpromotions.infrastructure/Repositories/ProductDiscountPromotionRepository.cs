using app.shoppingpromotions.infrastructure.Entities;
using app.shoppingpromotions.infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace app.shoppingpromotions.infrastructure.Repositories
{
    public class ProductDiscountPromotionRepository : IProductDiscountPromotionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductDiscountPromotionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DiscountPromotion> GetActiveDiscountPromotionAsync(string productId, DateTime transactionDate)
        {
            return await (from dp in _dbContext.DiscountPromotions
                          join dpp in _dbContext.DiscountPromotionProducts
                          on dp.DiscountPromotionId equals dpp.DiscountPromotionId
                          where dp.StartDate <= transactionDate && dp.EndDate >= transactionDate && string.Equals(dpp.ProductId, productId)
                          select dp).FirstOrDefaultAsync();
        }
    }
}
