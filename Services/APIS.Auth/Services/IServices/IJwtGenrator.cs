using APIS.Auth.Models;

namespace APIS.Auth.Services.IServices
{
	public interface IJwtGenrator
	{
		string GenrateToken(ApplicationUser ApplicationUser);
	}
}
