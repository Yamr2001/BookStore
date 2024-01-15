using APIS.Auth.Data;
using APIS.Auth.Models.Dto;
using APIS.Auth.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIS.Auth.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly IAuthServices _authservices;
        private readonly ResponseDto _responseDto;

        public AuthController(ApplicationDbContext context, IAuthServices authservices)
        {
            _context = context;
            _authservices = authservices;
            _responseDto = new();
        }

        [HttpPost("Registeration")]
        public async Task<IActionResult> Registeration ([FromBody]RegisterationRequestDto model)
        {
            var errormessage = await _authservices.Registration(model);
            if (!string.IsNullOrEmpty(errormessage)) {

                _responseDto.IsSucsses = false;
                _responseDto.Message = errormessage;
            
            }
            return Ok(_responseDto);
        }
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
		{
			var loginResponse = await _authservices.Login(model);
			if (loginResponse.UserDto == null)
			{
				_responseDto.IsSucsses = false;
				_responseDto.Message = "User Name or Password is inncorrect";

			}
            _responseDto.Results = loginResponse;
			return Ok(_responseDto);
		}
	}
}
