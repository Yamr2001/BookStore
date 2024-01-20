using APIS.Web.Models.Dto;
using APIS.Web.Services.IServices;
using static APIS.Web.Utility.SD;
using System.Net;
using Newtonsoft.Json;
using System.Text;

namespace APIS.Web.Services
{
    public class BaseServices : IBaseServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider _tokenProvider;
        public BaseServices(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        {
            _httpClientFactory = httpClientFactory;
            _tokenProvider = tokenProvider;
        }

        public async Task<ResponseDto?> SendAsync(RequestDto requestDto,bool WithBearer)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("APICLIENT");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                if (WithBearer)
                {
                    var token = _tokenProvider.GetToken();
                    message.Headers.Add("Authroiztion", $"Bearer {token}");
                }
                message.RequestUri = new Uri(requestDto.Url);
                if (requestDto.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                }
                HttpResponseMessage? apiResponse = null;

                switch (requestDto.APITYPE)
                {
                    case APITYPE.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case APITYPE.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    case APITYPE.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                apiResponse = await client.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSucsses = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new() { IsSucsses = false, Message = "Access Denied" };
                    case HttpStatusCode.Unauthorized:
                        return new() { IsSucsses = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new() { IsSucsses = false, Message = "Internal Server Error" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return apiResponseDto;
                }
            }
            catch (Exception ex)
            {
                var dto = new ResponseDto
                {
                    IsSucsses = false,
                    Message = ex.Message,
                };
                return dto;
            }
        }
    }
}
