using Microsoft.AspNetCore.Mvc;
using WEB_253551_Levchuk.Services;
using WEB_253551_Levchuk.Models;

namespace WEB_253551_Levchuk.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(string? category, int pageNo = 1)
        {
            var productResponse = await _productService.GetProductListAsync(category, pageNo);
            
            if (!productResponse.SuccessFull)
            {
                return NotFound(productResponse.ErrorMessage);
            }

            // Получаем список категорий для фильтра
            var categoriesResponse = await _categoryService.GetCategoryListAsync();
            
            ViewData["categories"] = categoriesResponse.Data ?? new List<Category>();
            ViewData["currentCategory"] = category;

            return View(productResponse.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var productResponse = await _productService.GetProductByIdAsync(id);
            
            if (!productResponse.SuccessFull || productResponse.Data == null)
            {
                return NotFound(productResponse.ErrorMessage);
            }

            return View(productResponse.Data);
        }
    }
}
