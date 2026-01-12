using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using WEB_253551_Levchuk.BlazorUI.Models;

namespace WEB_253551_Levchuk.BlazorUI.Services
{
    public class DataService : IDataService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _serializerOptions;

        public DataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task<ResponseData<ListModel<Category>>> GetCategoryListAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/categories");
                
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<ResponseData<ListModel<Category>>>(_serializerOptions);
                    return data ?? new ResponseData<ListModel<Category>>
                    {
                        SuccessFull = false,
                        ErrorMessage = "Failed to deserialize response"
                    };
                }
                
                return new ResponseData<ListModel<Category>>
                {
                    SuccessFull = false,
                    ErrorMessage = $"Error: {response.StatusCode}"
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<ListModel<Category>>
                {
                    SuccessFull = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ResponseData<ListModel<Dish>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            try
            {
                var url = $"api/dishes?pageNo={pageNo}&pageSize=3";
                if (!string.IsNullOrEmpty(categoryNormalizedName))
                {
                    url += $"&categoryNormalizedName={categoryNormalizedName}";
                }

                var response = await _httpClient.GetAsync(url);
                
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<ResponseData<ListModel<Dish>>>(_serializerOptions);
                    return data ?? new ResponseData<ListModel<Dish>>
                    {
                        SuccessFull = false,
                        ErrorMessage = "Failed to deserialize response"
                    };
                }
                
                return new ResponseData<ListModel<Dish>>
                {
                    SuccessFull = false,
                    ErrorMessage = $"Error: {response.StatusCode}"
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<ListModel<Dish>>
                {
                    SuccessFull = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ResponseData<Dish>> GetProductByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/dishes/{id}");
                
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<ResponseData<Dish>>(_serializerOptions);
                    return data ?? new ResponseData<Dish>
                    {
                        SuccessFull = false,
                        ErrorMessage = "Failed to deserialize response"
                    };
                }
                
                return new ResponseData<Dish>
                {
                    SuccessFull = false,
                    ErrorMessage = $"Error: {response.StatusCode}"
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<Dish>
                {
                    SuccessFull = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}

