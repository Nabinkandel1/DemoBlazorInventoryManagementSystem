using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public async Task<ActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _productRepository.AddAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest("Product ID mismatch");

            try
            {
                await _productRepository.UpdateAsync(product);
                return NoContent();
            }
            catch
            {
                return StatusCode(500, "An error occurred while updating the product.");
            }
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
                return NotFound();

            await _productRepository.DeleteAsync(id);
            return NoContent();
        }

        // GET: api/products/lowstock/{threshold}
        [HttpGet("lowstock/{threshold}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetLowStock(int threshold)
        {
            var lowStockProducts = await _productRepository.GetLowStockAsync(threshold);
            return Ok(lowStockProducts);
        }
    }
}

