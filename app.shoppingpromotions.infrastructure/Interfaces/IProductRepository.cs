using app.shoppingpromotions.infrastructure.Entities;

namespace app.shoppingpromotions.infrastructure.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(string productId);
    }
}