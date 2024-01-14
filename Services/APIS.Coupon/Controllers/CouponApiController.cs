using APIS.Coupon.Data;
using APIS.Coupon.Models.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIS.Coupon.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ResponseDto _responseDto;

        public CouponApiController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _responseDto = new();
        }

        [HttpGet]
        public async Task<ResponseDto> Get()
        {
            try
            {
                IEnumerable<Coupon> couponDtos = _context.Coupons.ToList();
                _responseDto.Results = _mapper.Map<IEnumerable<CouponDto>>(couponDtos);    

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
        public async Task<ResponseDto> GetbyId(int id)
        {
            try
            {
                Coupon coupon = _context.Coupons.FirstOrDefault(C=>C.CouponId == id);
                _responseDto.Results = _mapper.Map<CouponDto>(coupon);

            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.IsSucsses = false;
            }
            return _responseDto;
        }
        [HttpGet]
        [Route("GetbyCode/{code}")]
        public async Task<ResponseDto> GetbyCode(string code)
        {
            try
            {
                Coupon coupon = _context.Coupons.FirstOrDefault(C => C.CouponCode == code);
                _responseDto.Results = _mapper.Map<CouponDto>(coupon);

            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.IsSucsses = false;
            }
            return _responseDto;
        }
        [HttpPost]
        public async Task<ResponseDto> POST(CouponDto model)
        {
            try
            {
                Coupon coupon = _mapper.Map<Coupon>(model);
                _context.Coupons.Add(coupon);
                _context.SaveChanges();
                _responseDto.Results = _mapper.Map<Coupon>(model);

            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.IsSucsses = false;
            }
            return _responseDto;
        }
        [HttpPut]
        public async Task<ResponseDto> PUT(CouponDto model)
        {
            try
            {
                Coupon coupon = _mapper.Map<Coupon>(model);
                _context.Coupons.Update(coupon);
                _context.SaveChanges();
                _responseDto.Results = _mapper.Map<Coupon>(model);

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
        public async Task<ResponseDto> Delete(int id)
        {
            try
            {
                Coupon coupon = _context.Coupons.FirstOrDefault(C=>C.CouponId== id);
                _context.Coupons.Remove(coupon);
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
