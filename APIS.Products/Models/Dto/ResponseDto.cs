namespace APIS.Products.Models.Dto
{
    public class ResponseDto
    {
        public object? Results { get; set; }
        public bool IsSucsses { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
