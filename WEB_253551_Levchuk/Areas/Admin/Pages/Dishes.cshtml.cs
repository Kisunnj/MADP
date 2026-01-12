using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_253551_Levchuk.Models;
using WEB_253551_Levchuk.Services;

namespace WEB_253551_Levchuk.Areas.Admin.Pages
{
    [Authorize(Roles = "admin")]
    public class DishesModel : PageModel
    {
        private readonly IProductService _productService;

        public DishesModel(IProductService productService)
        {
            _productService = productService;
        }

        public List<Dish> Dishes { get; set; } = new();

        public async Task OnGetAsync()
        {
            var result = await _productService.GetProductListAsync(null, 1);
            if (result.SuccessFull && result.Data != null)
            {
                Dishes = result.Data.Items;
            }
        }
    }
}

