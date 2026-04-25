using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery] ProductSpecParams specParams)
        {
            var spec = new ProductSpecification(specParams);
            var products = await _unitOfWork.Repository<Product>().ListAsync(spec);
            var count = await _unitOfWork.Repository<Product>().CountAsync(spec);

            var pagination = new Pagination<Product>(specParams.PageIndex, specParams.PageSize, count, products);
            return Ok(pagination);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product productsDto)
        {
            if (productsDto == null)
                return BadRequest();

            _unitOfWork.Repository<Product>().Add(productsDto);

            if (await _unitOfWork.Complete())
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

            _unitOfWork.Repository<Product>().Update(productDto);

            if (await _unitOfWork.Complete())
                return Created("Updated", productDto);

            return BadRequest("Problem in update product");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProductById(int id)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(id);
            if (product == null)
                return BadRequest();

            _unitOfWork.Repository<Product>().Remove(product);
            if (await _unitOfWork.Complete())
                return Created("Product is deleted", product);

            return BadRequest("Problem in delete product");
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            var spec =  new BrandListSpecification();
            return Ok(await _unitOfWork.Repository<Product>().ListAsync(spec));
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            var spec = new TypeListSpecification();
            return Ok(await _unitOfWork.Repository<Product>().ListAsync(spec));
        }

        private bool ProductExists(int id)
        {
            return _unitOfWork.Repository<Product>().Exists(id);
        }
    }
}
