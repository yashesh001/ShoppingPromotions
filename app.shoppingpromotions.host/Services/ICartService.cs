using app.shoppingpromotions.domain;

namespace app.shoppingpromotions.host.Services
{
    public interface ICartService
    {
        Task<ShoppingResponse> AddItem(ShoppingRequest request);
        Task<ShoppingResponse> RemoveItem(ShoppingRequest request);
        int GetTotalItemsCount(string customerId);
    }
}
