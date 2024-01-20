using APIS.ShoppingCart.Data;
using APIS.ShoppingCart.Models;
using APIS.ShoppingCart.Models.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIS.ShoppingCart.Controllers
{
    [Route("api/ShoppingCart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ResponseDto _responseDto;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public ShoppingCartController(IMapper mapper, ApplicationDbContext context)
        {
            _responseDto = new();
            _mapper = mapper;
            _context = context;
        }

        [HttpPost]
        public async Task<ResponseDto> CartUpsert(CartDto cartDto)
        {
            try
            {
                var CartHeaderFromDb = await _context.cartHeaders.AsNoTracking()
                    .FirstOrDefaultAsync(u=>u.UserId == cartDto.CartHeader.UserId);
                if (CartHeaderFromDb == null)
                {
                    //create
                    CartHeader cartHeader = _mapper.Map<CartHeader>(cartDto.CartHeader);
                    _context.cartHeaders.Add(cartHeader);
                    await _context.SaveChangesAsync();
                    cartDto.CartDetailsDtos.First().CartHeaderId = cartHeader.CartHeaderId;
                    _context.cartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetailsDtos.First()));
                    await _context.SaveChangesAsync();

                }
                else
                {
                    // Update
                    var cartDetailsFromDb = await _context.cartDetails.AsNoTracking()
                        .FirstOrDefaultAsync(u=>u.ProductId == cartDto.CartDetailsDtos.First().ProductId && 
                    u.CartHeaderId == CartHeaderFromDb.CartHeaderId
                    );
                    if(cartDetailsFromDb == null)
                    {
                        cartDto.CartDetailsDtos.First().CartHeaderId = CartHeaderFromDb.CartHeaderId;
                        _context.cartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetailsDtos.First()));
                         await _context.SaveChangesAsync();
                    }
                    else
                    {
                        cartDto.CartDetailsDtos.First().Count += cartDetailsFromDb.Count;
                        cartDto.CartDetailsDtos.First().CartHeaderId = cartDetailsFromDb.CartHeaderId;
                        cartDto.CartDetailsDtos.First().CartDetailsId = cartDetailsFromDb.CartDetailsId;
                        _context.cartDetails.Update(_mapper.Map<CartDetails>(cartDto.CartDetailsDtos.First()));
                        await _context.SaveChangesAsync();
                    }
                    _responseDto.Results = cartDto;
                }
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
