using WEB_253551_Levchuk.BlazorUI.Models;

namespace WEB_253551_Levchuk.BlazorUI.Services
{
    public interface IDataService
    {
        Task<ResponseData<ListModel<Category>>> GetCategoryListAsync();
        Task<ResponseData<ListModel<Dish>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1);
        Task<ResponseData<Dish>> GetProductByIdAsync(int id);
    }
}

