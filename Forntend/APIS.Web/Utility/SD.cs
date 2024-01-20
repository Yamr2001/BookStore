namespace APIS.Web.Utility
{
    public class SD
    {
        public static string APICouponBase {  get; set; }   
        public static string APIAuthBase {  get; set; }

        public const string Admin  = "Admin";
        public const string Customer  = "Customer";
        public const string CookieToken = "JWTCOOKIE";
        public enum APITYPE
        {
            GET,
            POST,
            DELETE,
            PUT
        }
    }
}
