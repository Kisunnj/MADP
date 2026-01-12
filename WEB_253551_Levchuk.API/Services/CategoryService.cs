using Microsoft.EntityFrameworkCore;
using WEB_253551_Levchuk.API.Data;
using WEB_253551_Levchuk.API.Models;

namespace WEB_253551_Levchuk.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return ResponseData<List<Category>>.Success(categories);
        }
    }
}

