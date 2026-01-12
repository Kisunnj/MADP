using Microsoft.EntityFrameworkCore;
using WEB_253551_Levchuk.API.Models;

namespace WEB_253551_Levchuk.API.Data
{
    public static class DbInitializer
    {
        public static async Task SeedData(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Выполнить миграции
            await context.Database.MigrateAsync();

            // Если данные уже есть, ничего не делать
            if (await context.Categories.AnyAsync())
            {
                return;
            }

            // Добавить категории
            var categories = new List<Category>
            {
                new Category { Name = "Стартеры", NormalizedName = "starters" },
                new Category { Name = "Салаты", NormalizedName = "salads" },
                new Category { Name = "Супы", NormalizedName = "soups" },
                new Category { Name = "Основные блюда", NormalizedName = "main-dishes" },
                new Category { Name = "Напитки", NormalizedName = "drinks" },
                new Category { Name = "Десерты", NormalizedName = "desserts" }
            };

            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();

            // Добавить блюда
            var soups = categories.First(c => c.NormalizedName == "soups");
            var dishes = new List<Dish>
            {
                new Dish 
                { 
                    Name = "Суп-харчо", 
                    Description = "Очень острый, невкусный",
                    Calories = 200,
                    CategoryId = soups.Id,
                    Image = "Images/Суп.jpg"
                },
                new Dish 
                { 
                    Name = "Борщ", 
                    Description = "Много сала, без сметаны",
                    Calories = 330,
                    CategoryId = soups.Id,
                    Image = "Images/Борщ.jpg"
                }
            };

            await context.Dishes.AddRangeAsync(dishes);
            await context.SaveChangesAsync();
        }
    }
}

