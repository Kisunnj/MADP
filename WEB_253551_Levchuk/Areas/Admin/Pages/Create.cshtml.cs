using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WEB_253551_Levchuk.Models;
using WEB_253551_Levchuk.Services;

namespace WEB_253551_Levchuk.Areas.Admin.Pages
{
    [Authorize(Roles = "admin")]
    public class CreateModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _webHost;

        public CreateModel(
            IProductService productService,
            ICategoryService categoryService,
            IWebHostEnvironment webHost)
        {
            _productService = productService;
            _categoryService = categoryService;
            _webHost = webHost;
        }

        [BindProperty]
        public DishInputModel Dish { get; set; } = new();

        public SelectList CategoryList { get; set; } = new SelectList(new List<Category>(), "Id", "Name");

        public async Task OnGetAsync()
        {
            await LoadCategories();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile? formFile)
        {
            if (!ModelState.IsValid)
            {
                await LoadCategories();
                return Page();
            }

            var dish = new Dish
            {
                Name = Dish.Name,
                Description = Dish.Description,
                Calories = Dish.Calories,
                CategoryId = Dish.CategoryId
            };

            // Сохранить файл изображения
            if (formFile != null && formFile.Length > 0)
            {
                var imagePath = Path.Combine(_webHost.WebRootPath, "Images");
                
                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }

                var fileName = Path.GetRandomFileName() + Path.GetExtension(formFile.FileName);
                var filePath = Path.Combine(imagePath, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }

                dish.Image = $"Images/{fileName}";
            }

            var result = await _productService.CreateProductAsync(dish);
            
            if (!result.SuccessFull)
            {
                ModelState.AddModelError("", result.ErrorMessage ?? "Ошибка создания блюда");
                await LoadCategories();
                return Page();
            }

            return RedirectToPage("Dishes");
        }

        private async Task LoadCategories()
        {
            var categoriesResult = await _categoryService.GetCategoryListAsync();
            if (categoriesResult.SuccessFull && categoriesResult.Data != null)
            {
                CategoryList = new SelectList(categoriesResult.Data, "Id", "Name");
            }
        }
    }

    public class DishInputModel
    {
        [Required(ErrorMessage = "Название обязательно")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Описание обязательно")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Калории обязательны")]
        [Range(0, 10000, ErrorMessage = "Калории должны быть от 0 до 10000")]
        public int Calories { get; set; }

        [Required(ErrorMessage = "Выберите категорию")]
        public int CategoryId { get; set; }
    }
}

