using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WEB_253551_Levchuk.Areas.Admin.Pages
{
    [Authorize(Roles = "admin")]
    public class IndexModel : PageModel
    {
        public int TotalUsers { get; set; } = 15;
        public int TotalProducts { get; set; } = 5;
        public int TotalOrders { get; set; } = 23;
        public decimal TotalRevenue { get; set; } = 12500.00M;
        
        public void OnGet()
        {
            // Здесь можно загрузить реальные данные из базы данных
        }
    }
}

