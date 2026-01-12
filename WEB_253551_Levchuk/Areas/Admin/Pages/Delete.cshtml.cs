using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_253551_Levchuk.Models;
using WEB_253551_Levchuk.Services;

namespace WEB_253551_Levchuk.Areas.Admin.Pages
{
    [Authorize(Roles = "admin")]
    public class DeleteModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _webHost;

        public DeleteModel(IProductService productService, IWebHostEnvironment webHost)
        {
            _productService = productService;
            _webHost = webHost;
        }

        [BindProperty]
        public Dish? Dish { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var result = await _productService.GetProductByIdAsync(id);
            
            if (!result.SuccessFull || result.Data == null)
            {
                return NotFound();
            }

            Dish = result.Data;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var dishResult = await _productService.GetProductByIdAsync(id);
            
            if (!dishResult.SuccessFull || dishResult.Data == null)
            {
                return NotFound();
            }

            // Удалить файл изображения
            if (!string.IsNullOrEmpty(dishResult.Data.Image))
            {
                var imagePath = Path.Combine(_webHost.WebRootPath, dishResult.Data.Image);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            var result = await _productService.DeleteProductAsync(id);
            
            if (!result.SuccessFull)
            {
                ModelState.AddModelError("", result.ErrorMessage ?? "Ошибка удаления блюда");
                return Page();
            }

            return RedirectToPage("Dishes");
        }
    }
}

