namespace app.shoppingpromotions.host.Services
{
    public interface IPointsService
    {
        Task<int> GetPointsForProductAsync(string productCategory, DateTime transactionDate);
    }
}
