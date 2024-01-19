using APIS.Web.Services.IServices;
using APIS.Web.Utility;
using Newtonsoft.Json.Linq;

namespace APIS.Web.Services
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public TokenProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public void ClearToken()
        {
            _contextAccessor.HttpContext?.Response.Cookies.Delete(SD.CookieToken);
        }

        public string? GetToken()
        {
           string? Token = null;
           bool?hastoken = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(SD.CookieToken, out Token);

            return hastoken is true ?Token : null;   


        }

        public void SetToken(string Token)
        {
            _contextAccessor.HttpContext?.Response.Cookies.Append(SD.CookieToken,Token);
        }
    }
}
