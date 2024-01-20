using APIS.ShoppingCart.Models;
using APIS.ShoppingCart.Models.Dto;
using AutoMapper;

namespace APIS.ShoppingCart
{
    public static class MapperConfigrations
    {
        public static MapperConfiguration configuration ()
        {
            var mapp = new MapperConfiguration(Config =>
            {
                Config.CreateMap<CartHeader,CartHeaderDto>().ReverseMap();
                Config.CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
            });
            return mapp;
        }
    }
}
