using WEB_253551_Levchuk.Models;

namespace WEB_253551_Levchuk.Services
{
    public interface ICategoryService
    {
        /// <summary>
        /// Получение списка всех категорий
        /// </summary>
        Task<ResponseData<List<Category>>> GetCategoryListAsync();
    }
}

