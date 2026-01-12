using Microsoft.EntityFrameworkCore;
using WEB_253551_Levchuk.API.Data;
using WEB_253551_Levchuk.API.Models;

namespace WEB_253551_Levchuk.API.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHost;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(
            AppDbContext context, 
            IWebHostEnvironment webHost,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _webHost = webHost;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseData<ListModel<Dish>>> GetProductListAsync(
            string? categoryNormalizedName, 
            int pageNo = 1,
            int pageSize = 3)
        {
            var query = _context.Dishes.Include(d => d.Category).AsQueryable();

            if (!string.IsNullOrEmpty(categoryNormalizedName))
            {
                query = query.Where(d => d.Category != null && 
                    d.Category.NormalizedName!.Equals(categoryNormalizedName));
            }

            var count = await query.CountAsync();
            
            if (count == 0)
            {
                return ResponseData<ListModel<Dish>>.Success(new ListModel<Dish>());
            }

            int totalPages = (int)Math.Ceiling(count / (double)pageSize);

            if (pageNo > totalPages)
                return ResponseData<ListModel<Dish>>.Error("No such page");

            var dataList = new ListModel<Dish>
            {
                Items = await query
                    .OrderBy(d => d.Id)
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(),
                CurrentPage = pageNo,
                TotalPages = totalPages
            };

            return ResponseData<ListModel<Dish>>.Success(dataList);
        }

        public async Task<ResponseData<Dish>> GetProductByIdAsync(int id)
        {
            var dish = await _context.Dishes
                .Include(d => d.Category)
                .FirstOrDefaultAsync(d => d.Id == id);
            
            if (dish == null)
            {
                return ResponseData<Dish>.Error("Объект не найден");
            }

            return ResponseData<Dish>.Success(dish);
        }

        public async Task<ResponseData<Dish>> CreateProductAsync(Dish product, IFormFile? formFile)
        {
            // Сохранить файл изображения
            if (formFile != null)
            {
                var imageUrl = await SaveFileAsync(formFile);
                if (!string.IsNullOrEmpty(imageUrl))
                    product.Image = imageUrl;
            }

            _context.Dishes.Add(product);
            await _context.SaveChangesAsync();

            return ResponseData<Dish>.Success(product);
        }

        public async Task<ResponseData<Dish>> UpdateProductAsync(int id, Dish product, IFormFile? formFile)
        {
            var dish = await _context.Dishes.FindAsync(id);
            if (dish == null)
            {
                return ResponseData<Dish>.Error("Объект не найден");
            }

            // Сохранить новый файл изображения
            if (formFile != null)
            {
                // Удалить старый файл
                if (!string.IsNullOrEmpty(dish.Image))
                {
                    DeleteFile(dish.Image);
                }

                var imageUrl = await SaveFileAsync(formFile);
                if (!string.IsNullOrEmpty(imageUrl))
                    dish.Image = imageUrl;
            }

            dish.Name = product.Name;
            dish.Description = product.Description;
            dish.Calories = product.Calories;
            dish.CategoryId = product.CategoryId;

            await _context.SaveChangesAsync();

            return ResponseData<Dish>.Success(dish);
        }

        public async Task<ResponseData<Dish>> DeleteProductAsync(int id)
        {
            var dish = await _context.Dishes.FindAsync(id);
            if (dish == null)
            {
                return ResponseData<Dish>.Error("Объект не найден");
            }

            // Удалить файл изображения
            if (!string.IsNullOrEmpty(dish.Image))
            {
                DeleteFile(dish.Image);
            }

            _context.Dishes.Remove(dish);
            await _context.SaveChangesAsync();

            return ResponseData<Dish>.Success(dish);
        }

        private async Task<string> SaveFileAsync(IFormFile file)
        {
            var imagePath = Path.Combine(_webHost.WebRootPath, "Images");
            
            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }

            var fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(imagePath, fileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);

            var request = _httpContextAccessor.HttpContext?.Request;
            var host = $"{request?.Scheme}://{request?.Host}";
            var fileUrl = $"{host}/Images/{fileName}";

            return fileUrl;
        }

        private void DeleteFile(string imageUrl)
        {
            try
            {
                var fileName = Path.GetFileName(new Uri(imageUrl).LocalPath);
                var filePath = Path.Combine(_webHost.WebRootPath, "Images", fileName);
                
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch
            {
                // Игнорируем ошибки удаления
            }
        }
    }
}

