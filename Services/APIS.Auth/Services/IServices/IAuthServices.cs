using APIS.Auth.Models.Dto;

namespace APIS.Auth.Services.IServices
{
    public interface IAuthServices
    {
        Task<string> Registration(RegisterationRequestDto RegisterationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto LoginRequestDto);   
        Task<bool> AssignRole(string Email,string Role);
    }
}
