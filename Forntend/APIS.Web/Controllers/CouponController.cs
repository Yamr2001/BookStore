using APIS.Web.Models.Dto;
using APIS.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIS.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponServices _couponService;
        public CouponController(ICouponServices couponService)
        {
            _couponService = couponService;
        }


        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto>? list = new();

            ResponseDto? response = await _couponService.GetAllCouponsAsync();

            if (response != null)
            {
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Results));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(list);
        }

        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _couponService.CreateCouponsCodeAsync(model);

                if (response != null)
                {
                    TempData["success"] = "Coupon created successfully";
                    return RedirectToAction(nameof(CouponIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> CouponDelete(int couponId)
        {
            ResponseDto? response = await _couponService.GetCouponsIdAsync(couponId);

            if (response != null)
            {
                CouponDto? model = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Results));
                return View(model);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDto couponDto)
        {
            ResponseDto? response = await _couponService.DeleteCouponsCodeAsync(couponDto.CouponId);

            if (response != null)
            {
                TempData["success"] = "Coupon deleted successfully";
                return RedirectToAction(nameof(CouponIndex));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(couponDto);
        }

        [HttpGet]
        public async Task<IActionResult> CouponUpdate(int id)
        {
            ResponseDto? responseDto = await _couponService.GetCouponsIdAsync(id);
            if (responseDto != null)
            {
                CouponDto? couponDto = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(responseDto.Results));
                return View(couponDto);
            }
            else
            {
                TempData["error"] = responseDto?.Message;
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CouponUpdate(CouponDto couponDto)
        {
            ResponseDto responseDto = await _couponService.UpdateCouponsCodeAsync(couponDto);
            if (responseDto != null)
            {
                TempData["success"] = "Coupon Updated successfully";
                return RedirectToAction(nameof(CouponIndex));
            }
            else
            {
                TempData["error"] = responseDto?.Message;
            }
            return View(couponDto);
        }
    }
}
