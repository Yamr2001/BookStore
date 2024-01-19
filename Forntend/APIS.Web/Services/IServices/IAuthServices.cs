using APIS.Web.Models.Dto;

namespace APIS.Web.Services.IServices
{
    public interface IAuthServices
    {
        Task<ResponseDto?> Login(LoginRequestDto loginRequestDto);
        Task<ResponseDto?> Registeration(RegisterationRequestDto RegisterationRequestDto);
        Task<ResponseDto?> AssignRole(RegisterationRequestDto RegisterationRequestDto);
    }
}
