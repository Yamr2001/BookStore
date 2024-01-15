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

        public AuthServices(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManger)
        {
            _context = context;
            _userManager = userManager;
            _roleManger = roleManger;
        }

        public Task<LoginResponseDto> Login(LoginRequestDto LoginRequestDto)
        {
            throw new NotImplementedException();
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
