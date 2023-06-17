using app.shoppingpromotions.domain;
using app.shoppingpromotions.host.Services;
using app.shoppingpromotions.host.Validators;
using Microsoft.AspNetCore.Mvc;

namespace app.shoppingpromotions.host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ILogger<CartController> _logger;

        public CartController(ICartService basketService, ILogger<CartController> logger)
        {
            _cartService = basketService;
            _logger = logger;
        }

        [HttpPost("{customerId}")]
        [ProducesResponseType(typeof(ShoppingResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddCartItem(
            string customerId,
            [FromBody] ShoppingRequest request,
            [FromServices] ShoppingRequestValidator validator)
        {
            request.CustomerId = customerId;

            // Validate the request
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var response = await _cartService.AddItem(request);

            if (response == null)
            {
                return NotFound("Product ID provided was invalid.");
            }

            return Ok(response);
        }

        [HttpDelete("{customerId}")]
        [ProducesResponseType(typeof(ShoppingResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveCartItem(
            string customerId,
            [FromBody] ShoppingRequest request,
            [FromServices] ShoppingRequestValidator validator)
        {
            request.CustomerId = customerId;

            // Validate the request
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var response = await _cartService.RemoveItem(request);
            if (response == null)
            {
                return NotFound("Product ID provided was invalid.");
            }

            return Ok(response);
        }

        [HttpGet("{customerId}/count")]
        [ProducesResponseType(typeof(ShoppingResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetTotalItemsCount(string customerId)
        {
            decimal totalPrice = _cartService.GetTotalItemsCount(customerId);
            return Ok(totalPrice);
        }
    }
}