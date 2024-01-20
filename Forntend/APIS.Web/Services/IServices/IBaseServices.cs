using APIS.Web.Models.Dto;
using System.Diagnostics.Eventing.Reader;

namespace APIS.Web.Services.IServices
{
    public interface IBaseServices
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto,bool WithBearer = true); 
    }
}
