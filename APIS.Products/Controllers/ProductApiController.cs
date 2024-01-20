using APIS.Products.Data;
using APIS.Products.Models;
using APIS.Products.Models.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIS.Coupon.Controllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ResponseDto _responseDto;

        public ProductApiController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _responseDto = new();
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<ResponseDto> Get()
        {
            try
            {
                IEnumerable<Product> couponDtos = _context.Products.ToList();
                _responseDto.Results = _mapper.Map<IEnumerable<ProductDto>>(couponDtos);    

            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.IsSucsses = false;
            }
            return _responseDto;
        }
        [HttpGet]
        [Route("{id:int}")]
        //[Authorize(Roles = "Admin")]

        public async Task<ResponseDto> GetbyId(int id)
        {
            try
            {
                Product product = _context.Products.FirstOrDefault(C=>C.ProductId == id);
                _responseDto.Results = _mapper.Map<ProductDto>(product);

            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.IsSucsses = false;
            }
            return _responseDto;
        }
        [HttpPost]
       // [Authorize(Roles = "Admin")]

        public async Task<ResponseDto> POST(ProductDto model)
        {
            try
            {
                Product coupon = _mapper.Map<Product>(model);
                _context.Products.Add(coupon);
                _context.SaveChanges();
                _responseDto.Results = _mapper.Map<Product>(model);

            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.IsSucsses = false;
            }
            return _responseDto;
        }
        [HttpPut]
        //[Authorize(Roles = "Admin")]

        public async Task<ResponseDto> PUT(ProductDto model)
        {
            try
            {
                Product coupon = _mapper.Map<Product>(model);
                _context.Products.Update(coupon);
                _context.SaveChanges();
                _responseDto.Results = _mapper.Map<Product>(model);

            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.IsSucsses = false;
            }
            return _responseDto;
        }
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]

        public async Task<ResponseDto> Delete(int id)
        {
            try
            {
                Product coupon = _context.Products.FirstOrDefault(C=>C.ProductId== id);
                _context.Products.Remove(coupon);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.IsSucsses = false;
            }
            return _responseDto;
        }
    }
}
