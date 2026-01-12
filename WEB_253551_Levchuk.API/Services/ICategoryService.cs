using WEB_253551_Levchuk.API.Models;

namespace WEB_253551_Levchuk.API.Services
{
    public interface ICategoryService
    {
        Task<ResponseData<List<Category>>> GetCategoryListAsync();
    }
}

