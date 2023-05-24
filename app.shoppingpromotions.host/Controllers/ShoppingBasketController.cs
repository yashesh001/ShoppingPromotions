using app.shoppingpromotions.domain;
using app.shoppingpromotions.host.Services;
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
            [FromBody] ShoppingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _basketService.ComputeShoppersBasketAsync(request);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}