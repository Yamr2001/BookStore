using APIS.Web.Models.Dto;

namespace APIS.Web.Services.IServices
{
    public interface IBaseServices
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto); 
    }
}
