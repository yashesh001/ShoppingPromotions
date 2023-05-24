using app.shoppingpromotions.infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace app.shoppingpromotions.infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<PointsPromotion> PointsPromotions { get; set; }
        public DbSet<DiscountPromotion> DiscountPromotions { get; set; }
        public DbSet<DiscountPromotionProduct> DiscountPromotionProducts { get; set; }
    }
}