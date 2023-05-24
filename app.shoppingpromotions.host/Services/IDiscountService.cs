namespace app.shoppingpromotions.host.Services
{
    public interface IDiscountService
    {
        Task<decimal> GetDiscountForProductAsync(string productId, DateTime transactionDate);
    }
}
