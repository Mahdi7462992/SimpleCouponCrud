using SimpleCouponCrud.ApplicationServices.Dtos;
using SimpleCouponCrud.Frameworks;

namespace SimpleCouponCrud.ApplicationServices.Contracts
{
    public interface ICouponService
    {
        Task<ApiResult<PostCouponServiceDto>> Post(PostCouponServiceDto dto);
    }
}
