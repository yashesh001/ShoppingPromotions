using app.shoppingpromotions.infrastructure.Entities;

namespace app.shoppingpromotions.host.Services
{
    public interface IProductDiscountService
    {
        Task<decimal> GetDiscountForProductAsync(Product product, DateTime transactionDate);
    }
}
