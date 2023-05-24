using app.shoppingpromotions.infrastructure.Entities;
using app.shoppingpromotions.infrastructure.Interfaces;

namespace app.shoppingpromotions.infrastructure.Repositories
{
    public class StaticProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new()
        {
            new Product { ProductId = "PRD01", ProductName = "Vortex 95", Category = "Fuel", UnitPrice = 1.2m },
            new Product { ProductId = "PRD02", ProductName = "Vortex 98", Category = "Fuel", UnitPrice = 1.3m },
            new Product { ProductId = "PRD03", ProductName = "Diesel", Category = "Fuel", UnitPrice = 1.1m },
            new Product { ProductId = "PRD04", ProductName = "Twix 55g", Category = "Shop", UnitPrice = 2.3m },
            new Product { ProductId = "PRD05", ProductName = "Mars 72g", Category = "Shop", UnitPrice = 5.1m },
            new Product { ProductId = "PRD06", ProductName = "SNICKERS 72G", Category = "Shop", UnitPrice = 3.4m },
            new Product { ProductId = "PRD07", ProductName = "Bounty 3 63g", Category = "Shop", UnitPrice = 6.9m },
            new Product { ProductId = "PRD08", ProductName = "Snickers 50g", Category = "Shop", UnitPrice = 4.0m }
        };

        public async Task<Product> GetProductByIdAsync(string productId)
        {
            return await Task.FromResult(_products.FirstOrDefault(p => p.ProductId == productId));
        }
    }
}