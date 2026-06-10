using Microsoft.AspNetCore.Mvc;
using SimpleCouponCrud.ApplicationServices.Contracts;
using SimpleCouponCrud.ApplicationServices.Dtos;

namespace SimpleCouponCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        private readonly ICouponService _couponService;
        public CouponsController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostCouponServiceDto dto)
        {
            var result = await _couponService.Post(dto);
            return StatusCode((int)result.HttpStatusCode, result);
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _couponService.Delete(id);
            return StatusCode((int)result.HttpStatusCode, result);
        }

        [HttpPost("validate")]
        public async Task<IActionResult> Validate([FromBody] ValidateCouponDto dto)
        {
            var result = await _couponService.Validate(dto);
            return StatusCode((int)result.HttpStatusCode, result);
        }
    }
}
