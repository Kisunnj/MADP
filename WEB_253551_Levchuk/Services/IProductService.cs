using WEB_253551_Levchuk.Models;

namespace WEB_253551_Levchuk.Services
{
    public interface IProductService
    {
        /// <summary>
        /// Получение списка всех объектов
        /// </summary>
        /// <param name="categoryNormalizedName">Нормализованное имя категории для фильтрации</param>
        /// <param name="pageNo">Номер страницы списка</param>
        Task<ResponseData<ListModel<Dish>>> GetProductListAsync(
            string? categoryNormalizedName, 
            int pageNo = 1);

        /// <summary>
        /// Поиск объекта по Id
        /// </summary>
        /// <param name="id">Идентификатор объекта</param>
        Task<ResponseData<Dish>> GetProductByIdAsync(int id);

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="id">Id изменяемого объекта</param>
        /// <param name="product">Объект с новыми параметрами</param>
        Task<ResponseData<Dish>> UpdateProductAsync(int id, Dish product);

        /// <summary>
        /// Удаление объекта
        /// </summary>
        /// <param name="id">Id удаляемого объекта</param>
        Task<ResponseData<Dish>> DeleteProductAsync(int id);

        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="product">Новый объект</param>
        Task<ResponseData<Dish>> CreateProductAsync(Dish product);
    }
}

