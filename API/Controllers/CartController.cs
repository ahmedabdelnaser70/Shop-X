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
        public async Task<ActionResult<ShoppingCart>> GetCart(string cartId)
        {
            try
            {
                var cart = await _cartService.GetCartAsync(cartId);
                return Ok(cart ?? new ShoppingCart { Id = cartId });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> UpdateCart(ShoppingCart cart)
        {
            try
            {
                var updatedCart = await _cartService.SetCartAsync(cart);
                if (updatedCart == null)
                    return BadRequest("Problem saving cart.");

                return Ok(updatedCart);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCart(string cartId)
        {
            try
            {
                var deleted = await _cartService.DeleteCartAsync(cartId);
                if (!deleted) 
                    return BadRequest("Problem deleting cart.");

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
