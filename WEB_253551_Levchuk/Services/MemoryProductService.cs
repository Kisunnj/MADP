using WEB_253551_Levchuk.Models;

namespace WEB_253551_Levchuk.Services
{
    public class MemoryProductService : IProductService
    {
        private List<Dish> _dishes;
        private List<Category> _categories;
        private readonly int _maxPageSize = 20;

        public MemoryProductService(ICategoryService categoryService)
        {
            _categories = categoryService.GetCategoryListAsync()
                .Result
                .Data ?? new List<Category>();

            SetupData();
        }

        private void SetupData()
        {
            var starters = _categories.Find(c => c.NormalizedName == "starters");
            var salads = _categories.Find(c => c.NormalizedName == "salads");
            var soups = _categories.Find(c => c.NormalizedName == "soups");
            var mainDishes = _categories.Find(c => c.NormalizedName == "main-dishes");
            var drinks = _categories.Find(c => c.NormalizedName == "drinks");
            var desserts = _categories.Find(c => c.NormalizedName == "desserts");

            _dishes = new List<Dish>
            {
                // Стартеры
                new Dish { Id = 1, Name = "Брускетта", Description = "Хрустящий хлеб с помидорами и базиликом", 
                    Calories = 150, Image = "images/brusketta.jpeg", 
                    CategoryId = starters?.Id ?? 1, Category = starters },
                new Dish { Id = 2, Name = "Куриные крылышки", Description = "Острые крылышки в соусе барбекю", 
                    Calories = 280, Image = "images/crilishki.jpeg", 
                    CategoryId = starters?.Id ?? 1, Category = starters },
                new Dish { Id = 3, Name = "Креветки темпура", Description = "Креветки в хрустящем кляре", 
                    Calories = 320, Image = "images/crevetki.jpeg", 
                    CategoryId = starters?.Id ?? 1, Category = starters },
                new Dish { Id = 4, Name = "Сырные палочки", Description = "Моцарелла в панировке с соусом маринара", 
                    Calories = 380, Image = "images/chees.jpeg", 
                    CategoryId = starters?.Id ?? 1, Category = starters },
                
                // Салаты
                new Dish { Id = 5, Name = "Цезарь", Description = "Классический салат с курицей и пармезаном", 
                    Calories = 420, Image = "images/cesar.jpeg", 
                    CategoryId = salads?.Id ?? 2, Category = salads },
                new Dish { Id = 6, Name = "Греческий салат", Description = "Свежие овощи с фетой и оливками", 
                    Calories = 280, Image = "images/geak_salate.jpeg", 
                    CategoryId = salads?.Id ?? 2, Category = salads },
                new Dish { Id = 7, Name = "Капрезе", Description = "Моцарелла, томаты и базилик", 
                    Calories = 250, Image = "images/carpaze.jpeg", 
                    CategoryId = salads?.Id ?? 2, Category = salads },
                new Dish { Id = 8, Name = "Тёплый салат с говядиной", Description = "Говядина гриль с рукколой и овощами", 
                    Calories = 450, Image = "images/salate_beaf.jpeg", 
                    CategoryId = salads?.Id ?? 2, Category = salads },
                
                // Супы
                new Dish { Id = 9, Name = "Суп-харчо", Description = "Очень острый, невкусный", 
                    Calories = 200, Image = "images/harcho.jpeg", 
                    CategoryId = soups?.Id ?? 3, Category = soups },
                new Dish { Id = 10, Name = "Борщ", Description = "Много сала, без сметаны", 
                    Calories = 330, Image = "images/borsh.jpeg", 
                    CategoryId = soups?.Id ?? 3, Category = soups },
                new Dish { Id = 11, Name = "Том Ям", Description = "Острый тайский суп с морепродуктами", 
                    Calories = 180, Image = "images/tomyam.jpeg", 
                    CategoryId = soups?.Id ?? 3, Category = soups },
                new Dish { Id = 12, Name = "Французский луковый", Description = "Суп с карамелизированным луком и сыром", 
                    Calories = 350, Image = "images/mashromes.jpeg", 
                    CategoryId = soups?.Id ?? 3, Category = soups },
                new Dish { Id = 13, Name = "Крем-суп из грибов", Description = "Нежный крем-суп с лесными грибами", 
                    Calories = 220, Image = "images/creame_soap.jpeg", 
                    CategoryId = soups?.Id ?? 3, Category = soups },
                
                // Основные блюда
                new Dish { Id = 14, Name = "Стейк Рибай", Description = "Сочный стейк из мраморной говядины 300г", 
                    Calories = 680, Image = "images/rebay.jpeg", 
                    CategoryId = mainDishes?.Id ?? 4, Category = mainDishes },
                new Dish { Id = 15, Name = "Лосось на гриле", Description = "Филе лосося с овощами гриль", 
                    Calories = 520, Image = "images/lasos.jpeg", 
                    CategoryId = mainDishes?.Id ?? 4, Category = mainDishes },
                new Dish { Id = 16, Name = "Паста Карбонара", Description = "Спагетти с беконом и сливочным соусом", 
                    Calories = 720, Image = "images/karbonara.jpeg", 
                    CategoryId = mainDishes?.Id ?? 4, Category = mainDishes },
                new Dish { Id = 17, Name = "Куриное филе с картофелем", Description = "Запечённое филе с картофелем по-деревенски", 
                    Calories = 580, Image = "images/potato.jpeg", 
                    CategoryId = mainDishes?.Id ?? 4, Category = mainDishes },
                new Dish { Id = 18, Name = "Пицца Маргарита", Description = "Классическая пицца с моцареллой и томатами", 
                    Calories = 850, Image = "images/margarita.jpeg", 
                    CategoryId = mainDishes?.Id ?? 4, Category = mainDishes },
                new Dish { Id = 19, Name = "Ризотто с морепродуктами", Description = "Кремовое ризотто с креветками и мидиями", 
                    Calories = 620, Image = "images/risotto.jpeg", 
                    CategoryId = mainDishes?.Id ?? 4, Category = mainDishes },
                
                // Напитки
                new Dish { Id = 20, Name = "Свежевыжатый апельсиновый сок", Description = "Сок из свежих апельсинов 300мл", 
                    Calories = 120, Image = "images/orange_juse.jpeg", 
                    CategoryId = drinks?.Id ?? 5, Category = drinks },
                new Dish { Id = 21, Name = "Смузи манго-маракуйя", Description = "Освежающий смузи с тропическими фруктами", 
                    Calories = 180, Image = "images/smusi.jpeg", 
                    CategoryId = drinks?.Id ?? 5, Category = drinks },
                new Dish { Id = 22, Name = "Капучино", Description = "Классический капучино с молочной пенкой", 
                    Calories = 80, Image = "images/capucino.jpeg", 
                    CategoryId = drinks?.Id ?? 5, Category = drinks },
                new Dish { Id = 23, Name = "Лимонад домашний", Description = "Освежающий лимонад с мятой 400мл", 
                    Calories = 150, Image = "images/limonad.jpeg", 
                    CategoryId = drinks?.Id ?? 5, Category = drinks },
                new Dish { Id = 24, Name = "Чай зелёный", Description = "Зелёный чай с жасмином", 
                    Calories = 5, Image = "images/green_tea.jpeg", 
                    CategoryId = drinks?.Id ?? 5, Category = drinks },
                
                // Десерты
                new Dish { Id = 25, Name = "Тирамису", Description = "Классический итальянский десерт с маскарпоне", 
                    Calories = 450, Image = "images/tiramisu.jpeg", 
                    CategoryId = desserts?.Id ?? 6, Category = desserts },
                new Dish { Id = 26, Name = "Чизкейк Нью-Йорк", Description = "Нежный чизкейк с ягодным соусом", 
                    Calories = 520, Image = "images/cheescake.jpeg", 
                    CategoryId = desserts?.Id ?? 6, Category = desserts },
                new Dish { Id = 27, Name = "Шоколадный фондан", Description = "Горячий шоколадный десерт с жидкой начинкой", 
                    Calories = 580, Image = "images/fondan.jpeg", 
                    CategoryId = desserts?.Id ?? 6, Category = desserts },
                new Dish { Id = 28, Name = "Панна-котта", Description = "Итальянский десерт с ягодным соусом", 
                    Calories = 320, Image = "images/pinacota.jpeg", 
                    CategoryId = desserts?.Id ?? 6, Category = desserts },
                new Dish { Id = 29, Name = "Мороженое три вкуса", Description = "Ванильное, шоколадное и клубничное", 
                    Calories = 380, Image = "images/icecreame.jpeg", 
                    CategoryId = desserts?.Id ?? 6, Category = desserts },
                new Dish { Id = 30, Name = "Наполеон", Description = "Классический торт с заварным кремом", 
                    Calories = 420, Image = "images/napalion.jpeg", 
                    CategoryId = desserts?.Id ?? 6, Category = desserts }
            };
        }

        public Task<ResponseData<ListModel<Dish>>> GetProductListAsync(
            string? categoryNormalizedName, 
            int pageNo = 1)
        {
            var query = _dishes.AsQueryable();

            // Фильтрация по категории
            if (!string.IsNullOrEmpty(categoryNormalizedName))
            {
                query = query.Where(d => d.Category != null && 
                    d.Category.NormalizedName!.Equals(categoryNormalizedName));
            }

            // Количество элементов в списке
            var count = query.Count();
            
            if (count == 0)
            {
                return Task.FromResult(ResponseData<ListModel<Dish>>.Success(
                    new ListModel<Dish>()));
            }

            // Количество страниц
            int totalPages = (int)Math.Ceiling(count / (double)_maxPageSize);

            if (pageNo > totalPages)
                return Task.FromResult(ResponseData<ListModel<Dish>>.Error("No such page"));

            var dataList = new ListModel<Dish>
            {
                Items = query
                    .OrderBy(d => d.Id)
                    .Skip((pageNo - 1) * _maxPageSize)
                    .Take(_maxPageSize)
                    .ToList(),
                CurrentPage = pageNo,
                TotalPages = totalPages
            };

            return Task.FromResult(ResponseData<ListModel<Dish>>.Success(dataList));
        }

        public Task<ResponseData<Dish>> GetProductByIdAsync(int id)
        {
            var dish = _dishes.FirstOrDefault(d => d.Id == id);
            if (dish == null)
            {
                return Task.FromResult(ResponseData<Dish>.Error("Объект не найден"));
            }
            return Task.FromResult(ResponseData<Dish>.Success(dish));
        }

        public Task<ResponseData<Dish>> UpdateProductAsync(int id, Dish product)
        {
            var dish = _dishes.FirstOrDefault(d => d.Id == id);
            if (dish == null)
            {
                return Task.FromResult(ResponseData<Dish>.Error("Объект не найден"));
            }

            dish.Name = product.Name;
            dish.Description = product.Description;
            dish.Calories = product.Calories;
            dish.Image = product.Image;
            dish.CategoryId = product.CategoryId;
            dish.Category = _categories.FirstOrDefault(c => c.Id == product.CategoryId);

            return Task.FromResult(ResponseData<Dish>.Success(dish));
        }

        public Task<ResponseData<Dish>> DeleteProductAsync(int id)
        {
            var dish = _dishes.FirstOrDefault(d => d.Id == id);
            if (dish == null)
            {
                return Task.FromResult(ResponseData<Dish>.Error("Объект не найден"));
            }

            _dishes.Remove(dish);
            return Task.FromResult(ResponseData<Dish>.Success(dish));
        }

        public Task<ResponseData<Dish>> CreateProductAsync(Dish product)
        {
            product.Id = _dishes.Any() ? _dishes.Max(d => d.Id) + 1 : 1;
            product.Category = _categories.FirstOrDefault(c => c.Id == product.CategoryId);
            _dishes.Add(product);

            return Task.FromResult(ResponseData<Dish>.Success(product));
        }
    }
}

