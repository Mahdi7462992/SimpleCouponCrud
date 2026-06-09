using Microsoft.AspNetCore.Mvc;
using SimpleCouponCrud.ApplicationServices.Contracts;
using SimpleCouponCrud.ApplicationServices.Dtos;
using SimpleCouponCrud.Frameworks;

namespace SimpleCouponCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostCouponServiceDto dto)
        {
            var result = await _couponService.Post(dto);
            return StatusCode((int)result.HttpStatusCode, result);
        }
    }
}
