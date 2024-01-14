using static APIS.Web.Utility.SD;

namespace APIS.Web.Models.Dto
{
    public class RequestDto
    {
        public APITYPE APITYPE { get; set; } = APITYPE.GET;
        public string Url { get; set; } = string.Empty;
        public object Data { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
