using app.shoppingpromotions.domain;
using app.shoppingpromotions.infrastructure.Interfaces;

namespace app.shoppingpromotions.host.Services
{
    public class CartService : ICartService
    {
        private readonly IProductRepository _productRepository;
        private readonly IDiscountService _discountService;
        private readonly ICartRepository _cartRepository;
        private readonly IPointsService _pointsService;

        public CartService(IProductRepository productRepository, 
            IDiscountService discountService, 
            IPointsService pointsService,
            ICartRepository cartRepository)
        {
            _productRepository = productRepository;
            _discountService = discountService;
            _pointsService = pointsService;
            _cartRepository = cartRepository;
        }

        public async Task<ShoppingResponse> AddItem(ShoppingRequest request)
        {
            var product = await _productRepository.GetProductByIdAsync(request.ProductId)
                ?? throw new ProductNotFoundException(request.ProductId);   // Handle product not found error

            var existingItem = _cartRepository.GetCartItem(request.CustomerId, product.ProductId);
            if (existingItem == null)
            {
                _cartRepository.AddCartItem(request.CustomerId, new CartItem { ProductId = product.ProductId, Quantity = 1 });
            }
            else
            {
                existingItem.Quantity++;
                _cartRepository.UpdateCartItem(request.CustomerId, product.ProductId, existingItem);
            }

            return await ComputeCartAsync(request);
        }

        public async Task<ShoppingResponse> RemoveItem(ShoppingRequest request)
        {
            var itemToRemove = _cartRepository.GetCartItem(request.CustomerId, request.ProductId);
            if (itemToRemove != null)
            {
                if (itemToRemove.Quantity > 1)
                {
                    itemToRemove.Quantity--;
                    _cartRepository.UpdateCartItem(request.CustomerId, request.ProductId, itemToRemove);
                }
                else
                {
                    _cartRepository.RemoveCartItem(request.CustomerId, request.ProductId);
                }
            }

            return await ComputeCartAsync(request);
        }

        private async Task<ShoppingResponse> ComputeCartAsync(ShoppingRequest request)
        {
            decimal totalAmount = 0;
            decimal discountApplied = 0;
            int pointsEarned = 0;
            var cartItems = new List<ResponseCartItem>();

            var allCartItemsForCustomer = _cartRepository.GetCustomerCartItems(request.CustomerId);
            foreach (var item in allCartItemsForCustomer)
            {
                var product = await _productRepository.GetProductByIdAsync(item.ProductId);
                if (product == null)    //ToDo: Potentially a scenario where Product is out of stock / discontinued / not found etc.; Need to handle it appropriately.
                    continue;

                var cartItem = new ResponseCartItem
                {
                    ProductId = item.ProductId,
                    Price = product.UnitPrice,
                    Quantity = item.Quantity
                };

                totalAmount += cartItem.TotalAmount;

                // Apply discount
                cartItem.ItemDiscount = await _discountService.GetDiscountForProductAsync(product, request.TransactionDate);
                discountApplied += cartItem.TotalDiscount;

                // Calculate points earned
                int itemPoints = await _pointsService.GetPointsForProductAsync(product.Category, request.TransactionDate);
                pointsEarned += (int)(itemPoints * (product.UnitPrice * item.Quantity));

                cartItems.Add(cartItem);
            }

            decimal grandTotal = totalAmount - discountApplied;

            var response = new ShoppingResponse
            {
                CustomerId = request.CustomerId,
                TransactionDate = request.TransactionDate,
                TotalAmount = totalAmount,
                DiscountApplied = discountApplied,
                GrandTotal = grandTotal,
                PointsEarned = pointsEarned,
                CartItems = cartItems
            };

            return response;
        }

        public int GetTotalItemsCount(string customerId)
        {
            var allCartItemsForCustomer = _cartRepository.GetCustomerCartItems(customerId);

            return allCartItemsForCustomer == null ? 0 : allCartItemsForCustomer.Sum(item => item.Quantity);
        }
    }
}
