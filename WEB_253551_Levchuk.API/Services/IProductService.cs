using WEB_253551_Levchuk.API.Models;

namespace WEB_253551_Levchuk.API.Services
{
    public interface IProductService
    {
        Task<ResponseData<ListModel<Dish>>> GetProductListAsync(
            string? categoryNormalizedName, 
            int pageNo = 1,
            int pageSize = 3);

        Task<ResponseData<Dish>> GetProductByIdAsync(int id);
        Task<ResponseData<Dish>> UpdateProductAsync(int id, Dish product, IFormFile? formFile);
        Task<ResponseData<Dish>> DeleteProductAsync(int id);
        Task<ResponseData<Dish>> CreateProductAsync(Dish product, IFormFile? formFile);
    }
}

