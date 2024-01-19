using APIS.Web.Models.Dto;
using APIS.Web.Services.IServices;
using APIS.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace APIS.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthServices _authService;
        private readonly ITokenProvider _tokenProvider;
        public AuthController(IAuthServices authServices, ITokenProvider tokenProvider)
        {
            _authService = authServices;
            _tokenProvider = tokenProvider;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();
            return View(loginRequestDto);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto obj)
        {
            ResponseDto responseDto = await _authService.Login(obj);

            if (responseDto != null && responseDto.IsSucsses)
            {
                LoginResponseDto loginResponseDto =
                    JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Results));


                _tokenProvider.SetToken(loginResponseDto.Token);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = responseDto.Message;
                return View(obj);
            }
        }


        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text=SD.Admin,Value=SD.Admin},
                new SelectListItem{Text=SD.Customer,Value=SD.Customer},
            };

            ViewBag.RoleList = roleList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterationRequestDto obj)
        {
            ResponseDto result = await _authService.Registeration(obj);
            ResponseDto assingRole;

            if (result != null && result.IsSucsses)
            {
                if (string.IsNullOrEmpty(obj.Role))
                {
                    obj.Role = SD.Customer;
                }
                assingRole = await _authService.AssignRole(obj);
                if (assingRole != null && assingRole.IsSucsses)
                {
                    TempData["success"] = "Registration Successful";
                    return RedirectToAction(nameof(Login));
                }
            }
            else
            {
                TempData["error"] = result.Message;
            }

            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text=SD.Admin,Value = SD.Admin},
                new SelectListItem{Text=SD.Customer,Value=SD.Customer},
            };

            ViewBag.RoleList = roleList;
            return View(obj);
        }


    }
}

