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

        public async Task<ApiResult> Delete(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return new ApiResult(false, HttpStatusCode.BadRequest, ResponseMessage.NullInput);
                }

                var deleteResponse = await _couponRepository.Delete(id);
                if (!deleteResponse.IsSuccess)
                {
                    return new ApiResult(false, deleteResponse.HttpStatusCode, deleteResponse.Message);
                }

                return new ApiResult(true, HttpStatusCode.NoContent, ResponseMessage.SuccessfullOperation);
            }
            catch (Exception)
            {
                return new ApiResult(false, HttpStatusCode.InternalServerError, ResponseMessage.UnSuccessfullOperation);
            }
        }

        public async Task<ApiResult<PostCouponServiceDto>> Post(PostCouponServiceDto dto)
        {
            try
            {
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

                return new ApiResult<PostCouponServiceDto>(true, HttpStatusCode.OK, ResponseMessage.SuccessfullOperation, dto);
            }
            catch (Exception)
            {
                return new ApiResult<PostCouponServiceDto>(false, HttpStatusCode.InternalServerError, ResponseMessage.UnSuccessfullOperation, null);
            }
        }

        public async Task<ApiResult<ValidateCouponResultDto>> Validate(ValidateCouponDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return new ApiResult<ValidateCouponResultDto>(false, HttpStatusCode.BadRequest, ResponseMessage.NullInput, null);
                }

                var coupon = await _couponRepository.GetByCode(dto.CouponCode);
                if (coupon == null)
                {
                    return new ApiResult<ValidateCouponResultDto>(false, HttpStatusCode.NotFound, "Coupon not found.", null);
                }

                if (!coupon.IsActive)
                {
                    return new ApiResult<ValidateCouponResultDto>(false, HttpStatusCode.BadRequest, "Coupon is inactive.", null);
                }

                if (coupon.ExpirationDate < DateTime.UtcNow)
                {
                    return new ApiResult<ValidateCouponResultDto>(false, HttpStatusCode.BadRequest, "Coupon has expired.", null);
                }

                if (coupon.MinPurchaseAmount.HasValue && dto.PurchaseAmount < coupon.MinPurchaseAmount.Value)
                {
                    return new ApiResult<ValidateCouponResultDto>(false, HttpStatusCode.BadRequest, $"Minimum purchase amount is {coupon.MinPurchaseAmount}.", null);
                }

                decimal discountAmount = 0;
                if (coupon.DiscountType == DiscountType.Percentage)
                {
                    discountAmount = dto.PurchaseAmount * coupon.Value / 100;
                }
                else
                {
                    discountAmount = coupon.Value;
                }

                if (discountAmount > dto.PurchaseAmount)
                {
                    discountAmount = dto.PurchaseAmount;
                }

                var result = new ValidateCouponResultDto
                {
                    DiscountAmount = discountAmount,
                    FinalAmount = dto.PurchaseAmount - discountAmount
                };

                return new ApiResult<ValidateCouponResultDto>(true, HttpStatusCode.OK, ResponseMessage.SuccessfullOperation, result);
            }
            catch (Exception)
            {
                return new ApiResult<ValidateCouponResultDto>(false, HttpStatusCode.InternalServerError, ResponseMessage.UnSuccessfullOperation, null);
            }
        }
    }
}
