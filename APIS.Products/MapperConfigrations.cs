using APIS.Products.Models;
using APIS.Products.Models.Dto;
using AutoMapper;

namespace APIS.Products
{
    public static class MapperConfigrations
    {
        public static MapperConfiguration configuration ()
        {
            var mapp = new MapperConfiguration(Config =>
            {
                Config.CreateMap<Product,ProductDto>().ReverseMap();
                Config.CreateMap<ProductDto, Product>().ReverseMap();
            });
            return mapp;
        }
    }
}
