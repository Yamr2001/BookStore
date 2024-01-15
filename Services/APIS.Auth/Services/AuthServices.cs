using APIS.Auth.Data;
using APIS.Auth.Models;
using APIS.Auth.Models.Dto;
using APIS.Auth.Services.IServices;
using Microsoft.AspNetCore.Identity;

namespace APIS.Auth.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManger;
        private readonly IJwtGenrator _jwtGenrator;
        public AuthServices(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManger, IJwtGenrator jwtGenrator)
        {
            _context = context;
            _userManager = userManager;
            _roleManger = roleManger;
            _jwtGenrator = jwtGenrator;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto LoginRequestDto)
        {
            var user =  _context.ApplicationUsers.FirstOrDefault(x=>x.UserName == LoginRequestDto.UserName);
			bool isValid = await _userManager.CheckPasswordAsync(user, LoginRequestDto.Password);


			if (isValid == false && user != null)
            {
                return new LoginResponseDto { UserDto = null,Token = "" };
            }
            else
            {
				var token = _jwtGenrator.GenrateToken(user);
				UserDto userDto = new()
                {
                    ID = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber
                };


                LoginResponseDto loginResponseDto = new LoginResponseDto
                {
                    UserDto = userDto,
                    Token = token
                };
                return loginResponseDto;
            }

		}

        public async Task<string> Registration(RegisterationRequestDto RegisterationRequestDto)
        {
            try 
            { 

            ApplicationUser applicationUser = new ApplicationUser ()
            {
                UserName = RegisterationRequestDto.Email,
                Email = RegisterationRequestDto.Email,
                NormalizedEmail = RegisterationRequestDto.Email.ToUpper(),
                Name = RegisterationRequestDto.Name,
                PhoneNumber = RegisterationRequestDto.PhoneNumber
            };

            var result = await _userManager.CreateAsync(applicationUser, RegisterationRequestDto.Password);

            if(result.Succeeded)
            {

                    var usertoreturn = _context.ApplicationUsers.First(A => A.UserName == RegisterationRequestDto.Email);
                    UserDto userDto = new UserDto()
                    {
                        ID = usertoreturn.Id,
                        Email = usertoreturn.Email,
                        PhoneNumber = usertoreturn.PhoneNumber,
                        Name = usertoreturn.Name
                    };

                    return "";
            }
            }catch(Exception ex) { 
            
            
            }

            return "Error Is Occured";
        }
    }
}
