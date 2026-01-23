using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<ActionResult<ShoppingCart>> GetCartById(string id)
        {
            var cart = await _cartService.GetCartAsync(id);
            return Ok(cart ?? new ShoppingCart { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> UpdateCart(ShoppingCart cart)
        {
            var updatedCart = await _cartService.SetCartAsync(cart);
            if (updatedCart == null)
                return BadRequest("Problem saving cart.");

            return Ok(updatedCart);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCart(string id)
        {
            var deleted = await _cartService.DeleteCartAsync(id);
            if (!deleted)
                return BadRequest("Problem deleting cart.");

            return Ok();
        }
    }
}
