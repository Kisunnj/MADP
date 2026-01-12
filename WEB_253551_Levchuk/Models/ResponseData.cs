namespace WEB_253551_Levchuk.Models
{
    public class ResponseData<T>
    {
        // Запрашиваемые данные
        public T? Data { get; set; }
        
        // Признак успешного завершения запроса
        public bool SuccessFull { get; set; } = true;
        
        // Сообщение в случае ошибки
        public string? ErrorMessage { get; set; }
        
        // Методы для создания успешного ответа
        public static ResponseData<T> Success(T data)
        {
            return new ResponseData<T> { Data = data };
        }
        
        // Метод для создания ответа с ошибкой
        public static ResponseData<T> Error(string message, T? data = default)
        {
            return new ResponseData<T> 
            { 
                ErrorMessage = message, 
                SuccessFull = false, 
                Data = data 
            };
        }
    }
}

