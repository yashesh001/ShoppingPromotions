using app.shoppingpromotions.infrastructure.Entities;

namespace app.shoppingpromotions.host.Services
{
    public interface IDiscountService
    {
        Task<decimal> GetDiscountForProductAsync(Product product, DateTime transactionDate);
    }
}
