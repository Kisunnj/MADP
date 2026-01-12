namespace WEB_253551_Levchuk.API.Models
{
    public class ResponseData<T>
    {
        public T? Data { get; set; }
        public bool SuccessFull { get; set; } = true;
        public string? ErrorMessage { get; set; }
        
        public static ResponseData<T> Success(T data)
        {
            return new ResponseData<T> { Data = data };
        }
        
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

