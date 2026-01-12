using Microsoft.AspNetCore.Mvc;
using WEB_253551_Levchuk.API.Services;

namespace WEB_253551_Levchuk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _categoryService.GetCategoryListAsync();
            return Ok(result);
        }
    }
}

