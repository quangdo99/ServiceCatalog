using Microsoft.AspNetCore.Mvc;
using MongoExample.Models;
using MongoExample.Services;

namespace MongoExample.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly MongoDBServiceProduct _mongoDBService;

        public ProductController(MongoDBServiceProduct mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        [HttpGet]
        public async Task<List<Product>> Get()
        {
            return await _mongoDBService.GetAsync();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            await _mongoDBService.CreateAsync(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _mongoDBService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody] Product product)
        {
            await _mongoDBService.UpdateProduct(id, product);
            return NoContent();
        }

        [HttpPut("UpdateTag/{id}")]
        public async Task<IActionResult> AddToTaglist(string id, [FromBody] string tagStr)
        {
            await _mongoDBService.AddToTaglistAsync(id, tagStr);
            return NoContent();
        }
    }
}
