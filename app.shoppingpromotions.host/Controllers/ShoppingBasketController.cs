using app.shoppingpromotions.domain;
using app.shoppingpromotions.host.Services;
using app.shoppingpromotions.host.Validators;
using Microsoft.AspNetCore.Mvc;

namespace app.shoppingpromotions.host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingBasketController : ControllerBase
    {
        private readonly IShoppingBasketService _basketService;
        private readonly ILogger<ShoppingBasketController> _logger;

        public ShoppingBasketController(IShoppingBasketService basketService, ILogger<ShoppingBasketController> logger)
        {
            _basketService = basketService;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ComputeBasket(
            [FromBody] ShoppingRequest request,
            [FromServices] ShoppingRequestValidator validator)
        {
            // Validate the request
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var response = await _basketService.ComputeShoppersBasketAsync(request);

            if (response == null)
            {
                return NotFound("Product ID provided was invalid.");
            }

            return Ok(response);
        }
    }
}