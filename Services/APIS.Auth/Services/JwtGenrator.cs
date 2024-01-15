using APIS.Auth.Models;
using APIS.Auth.Services.IServices;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIS.Auth.Services
{
	public class JwtGenrator : IJwtGenrator
	{
		private readonly JwtOptions _jwtOptions;

		public JwtGenrator(IOptions <JwtOptions> jwtOptions)
		{
			_jwtOptions = jwtOptions.Value;
		}

		public string GenrateToken(ApplicationUser ApplicationUser)
		{
			var tokenhandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

			var cliamlist = new List<Claim>
			{
				new Claim (JwtRegisteredClaimNames.Email, ApplicationUser.Email),
				new Claim (JwtRegisteredClaimNames.Sub, ApplicationUser.Id),
				new Claim (JwtRegisteredClaimNames.Name, ApplicationUser.UserName),
			};

			var tokenDescriptior = new SecurityTokenDescriptor
			{
				Audience = _jwtOptions.Audience,
				Issuer = _jwtOptions.Issuer,
				Subject = new ClaimsIdentity(cliamlist),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenhandler.CreateToken(tokenDescriptior);
			return tokenhandler.WriteToken(token);
		}
	}
}
