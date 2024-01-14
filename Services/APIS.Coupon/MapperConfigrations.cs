using APIS.Coupon.Models.Dto;
using AutoMapper;

namespace APIS.Coupon
{
    public static class MapperConfigrations
    {
        public static MapperConfiguration configuration ()
        {
            var mapp = new MapperConfiguration(Config =>
            {
                Config.CreateMap<Coupon,CouponDto>().ReverseMap();
                Config.CreateMap<CouponDto, Coupon>().ReverseMap();
            });
            return mapp;
        }
    }
}
