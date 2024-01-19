namespace APIS.Web.Models.Dto
{
    public class LoginResponseDto
    {
        public UserDto UserDto { get; set; } = default!;
        public string Token { get; set; } = string.Empty;
    }
}
