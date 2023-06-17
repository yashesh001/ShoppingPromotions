using app.shoppingpromotions.domain;

namespace app.shoppingpromotions.infrastructure.Interfaces
{
    public interface ICartRepository
    {
        void AddCartItem(string customerId, CartItem cartItem);
        void RemoveCartItem(string customerId, string productId);
        void UpdateCartItem(string customerId, string productId, CartItem updatedCartItem);
        CartItem GetCartItem(string customerId, string productId);
        IEnumerable<CartItem> GetCustomerCartItems(string customerId);
    }
}
