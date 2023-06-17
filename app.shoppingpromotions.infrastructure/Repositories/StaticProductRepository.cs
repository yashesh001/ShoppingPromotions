﻿using app.shoppingpromotions.infrastructure.Entities;
using app.shoppingpromotions.infrastructure.Interfaces;

namespace app.shoppingpromotions.infrastructure.Repositories
{
    public class StaticProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new()
        {
            new Product { ProductId = "PRD01", ProductName = "Victoria Bitter", Category = "Fuel", UnitPrice = 21.49m },
            new Product { ProductId = "PRD02", ProductName = "Crown Lager", Category = "Fuel", UnitPrice = 22.99m },
            new Product { ProductId = "PRD03", ProductName = "Coopers", Category = "Fuel", UnitPrice = 20.49m },
            new Product { ProductId = "PRD04", ProductName = "Tooheys Extra Dry", Category = "Shop", UnitPrice = 19.99m }
        };

        public async Task<Product> GetProductByIdAsync(string productId)
        {
            return await Task.FromResult(_products.FirstOrDefault(p => p.ProductId == productId));
        }
    }
}