namespace APIS.Web.Utility
{
    public class SD
    {
        public static string APICouponBase {  get; set; }   
        public static string APIAuthBase {  get; set; }
        public static string Admin { get; set; } = "Admin";
        public static string Customer { get; set; } = "Customer";
        public static string CookieToken { get; set; } = "JWTCOOKIE";
        public enum APITYPE
        {
            GET,
            POST,
            DELETE,
            PUT
        }
    }
}
