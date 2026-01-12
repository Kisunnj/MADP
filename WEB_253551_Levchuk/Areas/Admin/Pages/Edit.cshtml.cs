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
    public class EditModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _webHost;

        public EditModel(
            IProductService productService,
            ICategoryService categoryService,
            IWebHostEnvironment webHost)
        {
            _productService = productService;
            _categoryService = categoryService;
            _webHost = webHost;
        }

        [BindProperty]
        public DishEditModel Dish { get; set; } = new();

        [BindProperty]
        public string? CurrentImage { get; set; }

        public SelectList CategoryList { get; set; } = new SelectList(new List<Category>(), "Id", "Name");

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var result = await _productService.GetProductByIdAsync(id);
            
            if (!result.SuccessFull || result.Data == null)
            {
                return NotFound();
            }

            Dish = new DishEditModel
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                Description = result.Data.Description,
                Calories = result.Data.Calories,
                CategoryId = result.Data.CategoryId
            };

            CurrentImage = result.Data.Image;

            await LoadCategories();
            return Page();
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
                Id = Dish.Id,
                Name = Dish.Name,
                Description = Dish.Description,
                Calories = Dish.Calories,
                CategoryId = Dish.CategoryId,
                Image = CurrentImage
            };

            // Сохранить новый файл изображения
            if (formFile != null && formFile.Length > 0)
            {
                // Удалить старое изображение
                if (!string.IsNullOrEmpty(CurrentImage))
                {
                    var oldImagePath = Path.Combine(_webHost.WebRootPath, CurrentImage);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

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

            var result = await _productService.UpdateProductAsync(Dish.Id, dish);
            
            if (!result.SuccessFull)
            {
                ModelState.AddModelError("", result.ErrorMessage ?? "Ошибка обновления блюда");
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

    public class DishEditModel
    {
        public int Id { get; set; }

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

