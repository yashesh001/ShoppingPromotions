using app.shoppingpromotions.domain;
using app.shoppingpromotions.infrastructure.Interfaces;
using Enyim.Caching;
using Enyim.Caching.Memcached;
using System.Text.Json;

namespace app.shoppingpromotions.infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ICacheWrapper _cache;
        private readonly TimeSpan _expirationDuration = TimeSpan.FromMinutes(10);

        public CartRepository(
            ICacheWrapper cache)
        {
            _cache = cache;
        }

        public void AddCartItem(string customerId, CartItem cartItem)
        {
            // Add the cart item to the cache
            string cacheKey = $"Cart:{customerId}:{cartItem.ProductId}";
            string cacheValue = Serialize(cartItem);

            _cache.Set(cacheKey, cacheValue, _expirationDuration);

            AddCartItemKey(customerId, cartItem.ProductId);
        }

        public void RemoveCartItem(string customerId, string productId)
        {
            // Remove the cart item from the cache
            string cacheKey = $"Cart:{customerId}:{productId}";
            _cache.Remove(cacheKey);

            RemoveCartItemKey(customerId, productId);
        }

        public void UpdateCartItem(string customerId, string productId, CartItem updatedCartItem)
        {
            // Deserialize the existing cart item
            var existingCartItem = GetCartItem(customerId, productId);

            // Update the cart item properties
            existingCartItem.Quantity = updatedCartItem.Quantity;

            // Serialize the updated cart item
            string updatedCartItemJson = Serialize(existingCartItem);

            // Store the updated cart item in the cache with the same cache key
            string cacheKey = $"Cart:{customerId}:{productId}";
            _cache.Set(cacheKey, updatedCartItemJson, _expirationDuration);
        }

        public CartItem GetCartItem(string customerId, string productId)
        {
            // Retrieve the cart item from the cache
            string cacheKey = $"Cart:{customerId}:{productId}";
            string cacheValue = _cache.Get<string>(cacheKey);

            if (cacheValue == null)
                return null;    // Cart item not found in cache

            // Reset the expiration of the cache item
            _cache.Set(cacheKey, cacheValue, _expirationDuration);

            // Deserialize and return the cart item
            return Deserialize<CartItem>(cacheValue);
        }

        public IEnumerable<CartItem> GetCustomerCartItems(string customerId)
        {
            var cartItemKeys = GetCartItemKeys(customerId);

            foreach (var cacheKey in cartItemKeys)
            {
                var cacheValue = _cache.Get<string>(cacheKey);

                if (cacheValue != null)
                {
                    _cache.Set(cacheKey, cacheValue, _expirationDuration);
                    yield return Deserialize<CartItem>(cacheValue);
                }
            }
        }

        #region Private Methods
        private void AddCartItemKey(string customerId, string productId)
        {
            string cartKey = $"Cart:{customerId}:{productId}";

            var cartItemKeys = GetCartItemKeys(customerId).ToList();
            cartItemKeys.Add(cartKey);

            _cache.Set(GetCartItemKeysCacheKey(customerId), cartItemKeys.ToArray(), _expirationDuration);
        }

        private void RemoveCartItemKey(string customerId, string productId)
        {
            string cartKey = $"Cart:{customerId}:{productId}";

            var cartItemKeys = GetCartItemKeys(customerId).ToList();
            cartItemKeys.Remove(cartKey);

            _cache.Set(GetCartItemKeysCacheKey(customerId), cartItemKeys.ToArray(), _expirationDuration);
        }

        private IEnumerable<string> GetCartItemKeys(string customerId)
        {
            string cartItemKeysCacheKey = GetCartItemKeysCacheKey(customerId);
            var cartItemKeys = _cache.Get<string[]>(cartItemKeysCacheKey);

            if (cartItemKeys != null)
            {
                _cache.Set(cartItemKeysCacheKey, cartItemKeys, _expirationDuration);
                return cartItemKeys;
            }

            return Enumerable.Empty<string>();
        }

        private static string GetCartItemKeysCacheKey(string customerId)
        {
            return $"CartKeys:{customerId}";
        }

        private static string Serialize<T>(T item)
        {
            return JsonSerializer.Serialize(item);
        }

        private static T Deserialize<T>(string itemString)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return JsonSerializer.Deserialize<T>(itemString);
#pragma warning restore CS8603 // Possible null reference return.
        } 
        #endregion
    }
}
