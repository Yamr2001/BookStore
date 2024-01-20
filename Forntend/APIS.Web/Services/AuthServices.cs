using APIS.Web.Models.Dto;
using APIS.Web.Services.IServices;
using APIS.Web.Utility;

namespace APIS.Web.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly IBaseServices _baseServices;

        public AuthServices(IBaseServices baseServices)
        {
            _baseServices = baseServices;
        }

        public async Task<ResponseDto?> AssignRole(RegisterationRequestDto RegisterationRequestDto)
        {
            return await _baseServices.SendAsync(new RequestDto
            {
                APITYPE = Utility.SD.APITYPE.POST,
                Data = RegisterationRequestDto,
                Url = SD.APIAuthBase+ "/api/auth/AssignRole"

            },WithBearer:false);
        }

        public async Task<ResponseDto?> Login(LoginRequestDto loginRequestDto)
        {
            return await _baseServices.SendAsync(new RequestDto
            {
                APITYPE = Utility.SD.APITYPE.POST,
                Data = loginRequestDto,
                Url = SD.APIAuthBase + "/api/auth/login"

            }, WithBearer: false);
        }

        public async Task<ResponseDto?> Registeration(RegisterationRequestDto RegisterationRequestDto)
        {
            return await _baseServices.SendAsync(new RequestDto
            {
                APITYPE = Utility.SD.APITYPE.POST,
                Data = RegisterationRequestDto,
                Url = SD.APIAuthBase + "/api/auth/Registeration"

            }, WithBearer: false);
        }
    }
}
