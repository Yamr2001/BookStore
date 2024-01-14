namespace APIS.Web.Models.Dto
{
    public class ResponseDto
    {
        public object? Results { get; set; }
        public bool IsSucsses { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}
