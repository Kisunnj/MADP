using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB_253551_Levchuk.API.Models;
using WEB_253551_Levchuk.API.Services;

namespace WEB_253551_Levchuk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        private readonly IProductService _productService;

        public DishesController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/dishes?categoryNormalizedName=soups&pageNo=1&pageSize=3
        [HttpGet]
        public async Task<IActionResult> GetDishes(
            string? categoryNormalizedName, 
            int pageNo = 1,
            int pageSize = 3)
        {
            var result = await _productService.GetProductListAsync(
                categoryNormalizedName, 
                pageNo, 
                pageSize);
            
            return Ok(result);
        }

        // GET: api/dishes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDish(int id)
        {
            var result = await _productService.GetProductByIdAsync(id);
            
            if (!result.SuccessFull)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        // POST: api/dishes
        [Authorize(Policy = "PowerUser")]
        [HttpPost]
        public async Task<IActionResult> CreateDish([FromForm] Dish dish, [FromForm] IFormFile? formFile)
        {
            var result = await _productService.CreateProductAsync(dish, formFile);
            
            if (!result.SuccessFull)
            {
                return BadRequest(result);
            }

            return CreatedAtAction(nameof(GetDish), new { id = result.Data!.Id }, result);
        }

        // PUT: api/dishes/5
        [Authorize(Policy = "PowerUser")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDish(int id, [FromForm] Dish dish, [FromForm] IFormFile? formFile)
        {
            var result = await _productService.UpdateProductAsync(id, dish, formFile);
            
            if (!result.SuccessFull)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        // DELETE: api/dishes/5
        [Authorize(Policy = "PowerUser")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDish(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            
            if (!result.SuccessFull)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}

