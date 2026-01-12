namespace WEB_253551_Levchuk.BlazorUI.Models
{
    public class ResponseData<T>
    {
        public bool SuccessFull { get; set; }
        public string? ErrorMessage { get; set; }
        public T? Data { get; set; }
    }
}

