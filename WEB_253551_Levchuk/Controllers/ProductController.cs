using Microsoft.AspNetCore.Mvc;
using WEB_253551_Levchuk.Models;

namespace WEB_253551_Levchuk.Controllers
{
    public class ProductController : Controller
    {
        // Временные данные для демонстрации
        private static List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Товар 1", Description = "Описание товара 1", Price = 100.00M, Category = "Категория А" },
            new Product { Id = 2, Name = "Товар 2", Description = "Описание товара 2", Price = 200.00M, Category = "Категория А" },
            new Product { Id = 3, Name = "Товар 3", Description = "Описание товара 3", Price = 150.00M, Category = "Категория Б" },
            new Product { Id = 4, Name = "Товар 4", Description = "Описание товара 4", Price = 300.00M, Category = "Категория Б" },
            new Product { Id = 5, Name = "Товар 5", Description = "Описание товара 5", Price = 250.00M, Category = "Категория В" }
        };

        public IActionResult Index()
        {
            return View(_products);
        }

        public IActionResult Details(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public IActionResult UserProducts()
        {
            // Здесь можно фильтровать продукты по пользователю
            return View(_products);
        }
    }
}

