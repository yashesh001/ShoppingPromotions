using app.shoppingpromotions.domain;

namespace app.shoppingpromotions.host.Services
{
    public interface IShoppingBasketService
    {
        Task<ShoppingResponse> ComputeShoppersBasketAsync(ShoppingRequest request);
    }
}
