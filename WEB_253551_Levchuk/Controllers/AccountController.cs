using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WEB_253551_Levchuk.Entities;

namespace WEB_253551_Levchuk.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Avatar(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null || user.Image == null)
            {
                // Возвращаем дефолтный аватар
                var defaultPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "avatar.png");
                if (System.IO.File.Exists(defaultPath))
                {
                    return PhysicalFile(defaultPath, "image/png");
                }
                return NotFound();
            }

            return File(user.Image, user.ContentType ?? "image/jpeg");
        }
    }
}

