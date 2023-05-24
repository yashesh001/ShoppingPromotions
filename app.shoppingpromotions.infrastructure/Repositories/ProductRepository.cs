using app.shoppingpromotions.infrastructure.Entities;
using app.shoppingpromotions.infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace app.shoppingpromotions.infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> GetProductByIdAsync(string productId)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
        }
    }
}
