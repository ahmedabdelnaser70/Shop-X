using API.Dtos;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductRepository _productRepo;
        public ProductsController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string brand, string type, string sort)
        {
            return Ok(await _productRepo.GetProductsAsync(brand, type, sort));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _productRepo.GetProductByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product productsDto)
        {
            if (productsDto == null)
                return BadRequest();

            _productRepo.AddProduct(productsDto);

            if (await _productRepo.SaveChangesAsync())
                return Created("added", productsDto);

            return BadRequest("Erorr adding product");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, Product productDto)
        {
            if (productDto.Id != id || !ProductExists(id))
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            _productRepo.UpdateProduct(productDto);

            if (await _productRepo.SaveChangesAsync())
                return Created("Updated", productDto);

            return BadRequest("Problem in update product");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProductById(int id)
        {
            var product = await _productRepo.GetProductByIdAsync(id);
            if (product == null)
                return BadRequest();

            _productRepo.DeleteProduct(product);
            if (await _productRepo.SaveChangesAsync())
                return Created("Product is deleted", product);

            return BadRequest("Problem in delete product");
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            return Ok(await _productRepo.GetBrandsAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            return Ok(await _productRepo.GetTypesAsync());
        }

        private bool ProductExists(int id)
        {
            return _productRepo.ProductExists(id);
        }
    }
}
