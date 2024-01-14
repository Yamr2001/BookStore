using APIS.Web.Models.Dto;

namespace APIS.Web.Services.IServices
{
    public interface ICouponServices
    {
        Task<ResponseDto?> GetAllCouponsAsync();    
        Task<ResponseDto?> GetCouponsIdAsync(int couponId);
        Task<ResponseDto?> GetCouponsCodeAsync(string couponCode);
        Task<ResponseDto?> CreateCouponsCodeAsync(CouponDto couponDto);
        Task<ResponseDto?> UpdateCouponsCodeAsync(CouponDto couponDto);
        Task<ResponseDto?> DeleteCouponsCodeAsync(int couponId);
    }
}
