using APIS.Web.Models.Dto;
using APIS.Web.Services.IServices;
using APIS.Web.Utility;

namespace APIS.Web.Services
{
    public class CouponServices : ICouponServices
    {
        private readonly IBaseServices _baseServices;

        public CouponServices(IBaseServices baseServices)
        {
            _baseServices = baseServices;
        }

        public async Task<ResponseDto?> CreateCouponsCodeAsync(CouponDto couponDto)
        {
            return await _baseServices.SendAsync(new RequestDto {
                APITYPE  = Utility.SD.APITYPE.POST,
                Data = couponDto,
                Url = SD.APICouponBase + "/api/coupon"
            });
        }

        public async Task<ResponseDto?> DeleteCouponsCodeAsync(int couponId)
        {
            return await _baseServices.SendAsync(new RequestDto
            {
                APITYPE = Utility.SD.APITYPE.DELETE,
                Url = SD.APICouponBase + "/api/coupon/" + couponId
            });
        }

        public async Task<ResponseDto?> GetAllCouponsAsync()
        {
            return await _baseServices.SendAsync(new RequestDto
            {
                APITYPE = Utility.SD.APITYPE.GET,
                Url = SD.APICouponBase + "/api/coupon"
            });
        }

        public async Task<ResponseDto?> GetCouponsCodeAsync(string couponCode)
        {
            return await _baseServices.SendAsync(new RequestDto
            {
                APITYPE = Utility.SD.APITYPE.GET,
                Url = SD.APICouponBase + "/api/coupon/GetbyCode/" + couponCode
            });
        }

        public async Task<ResponseDto?> GetCouponsIdAsync(int couponId)
        {
            return await _baseServices.SendAsync(new RequestDto
            {
                APITYPE = Utility.SD.APITYPE.GET,
                Url = SD.APICouponBase + "/api/coupon/" + couponId
            });
        }

        public async Task<ResponseDto?> UpdateCouponsCodeAsync(CouponDto couponDto)
        {
            return await _baseServices.SendAsync(new RequestDto
            {
                APITYPE = Utility.SD.APITYPE.PUT,
                Data = couponDto,
                Url = SD.APICouponBase + "/api/coupon"
            });
        }
    }
}
