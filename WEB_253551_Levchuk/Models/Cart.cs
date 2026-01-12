namespace WEB_253551_Levchuk.Models
{
    public class Cart
    {
        public bool IsPage { get; set; }
        public float? Cost { get; set; }
        public uint? Amount { get; set; }
        public string? Controller { get; set; }
        public string? Action { get; set; }
        public string? Page { get; set; }
        public string? Area { get; set; }
        public string? CostClassStyle { get; set; }
        public string? AmountClassStyle { get; set; }
        public string? AmountStyle { get; set; }
    }
}
