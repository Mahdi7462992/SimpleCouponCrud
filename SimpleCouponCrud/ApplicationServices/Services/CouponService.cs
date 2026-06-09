using SimpleCouponCrud.ApplicationServices.Contracts;
using SimpleCouponCrud.ApplicationServices.Dtos;
using SimpleCouponCrud.Frameworks;
using SimpleCouponCrud.Models.Entities;
using SimpleCouponCrud.Models.Services.Contracts;
using System.Net;

namespace SimpleCouponCrud.ApplicationServices.Services
{
    public class CouponService : ICouponService
    {
        private readonly ICouponRepository _couponRepository;
        public CouponService(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }
        public async Task<ApiResult<PostCouponServiceDto>> Post(PostCouponServiceDto dto)
        {
            if (dto == null)
            {
                return new ApiResult<PostCouponServiceDto>(false, HttpStatusCode.BadRequest, ResponseMessage.NullInput, null);
            }

            var existingCoupon = await _couponRepository.GetByCode(dto.Code);
            if (existingCoupon != null)
            {
                return new ApiResult<PostCouponServiceDto>(false, HttpStatusCode.Conflict, "Coupon code already exists.", dto);
            }
            var postedCoupon = new Coupon()
            {
                Id = Guid.NewGuid(),
                Code = dto.Code,
                DiscountType = dto.DiscountType,
                Value = dto.Value,
                ExpirationDate = dto.ExpirationDate,
                MinPurchaseAmount = dto.MinPurchaseAmount,
                IsActive = dto.IsActive,
            };
            var insertedResponse = await _couponRepository.Insert(postedCoupon);
            if (!insertedResponse.IsSuccess)
            {
                return new ApiResult<PostCouponServiceDto>(false, insertedResponse.HttpStatusCode, insertedResponse.Message, dto);
            }

            return new ApiResult<PostCouponServiceDto>(true, HttpStatusCode.Created, ResponseMessage.SuccessfullOperation, dto);
        }
    }
}
