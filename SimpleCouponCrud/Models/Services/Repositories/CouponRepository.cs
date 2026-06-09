using Microsoft.EntityFrameworkCore;
using SimpleCouponCrud.Frameworks;
using SimpleCouponCrud.Models.Entities;
using SimpleCouponCrud.Models.Services.Contracts;
using System.Net;

namespace SimpleCouponCrud.Models.Services.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly ProjectDbContext _context;
        public CouponRepository(ProjectDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResult<Coupon>> Delete(Coupon obj)
        {
            try
            {
                if (obj is null)
                {
                    return new ApiResult<Coupon>(false, HttpStatusCode.BadRequest, ResponseMessage.NullInput, null);
                }

                var coupon = await _context.Coupons.FindAsync(obj.Id);
                if (coupon == null)
                {
                    return new ApiResult<Coupon>(false, HttpStatusCode.BadRequest, ResponseMessage.NullInput, null);
                }

                _context.Coupons.Remove(coupon);
                await _context.SaveChangesAsync();
                return new ApiResult<Coupon>(true, HttpStatusCode.OK, ResponseMessage.SuccessfullOperation, coupon);
            }
            catch (Exception)
            {
                return new ApiResult<Coupon>(false, HttpStatusCode.InternalServerError, ResponseMessage.UnSuccessfullOperation, null);
            }
        }

        public async Task<Coupon?> GetByCode(string code)
        {
            var result = await _context.Coupons
                 .FirstOrDefaultAsync(x => x.Code == code);
            return result;
        }

        public async Task<ApiResult<Coupon>> Insert(Coupon obj)
        {
            try
            {
                if (obj is null)
                {
                    return new ApiResult<Coupon>(false, HttpStatusCode.BadRequest, ResponseMessage.NullInput, null);
                }
                await _context.Coupons.AddAsync(obj);
                await _context.SaveChangesAsync();
                return new ApiResult<Coupon>(true, HttpStatusCode.Created, ResponseMessage.SuccessfullOperation, obj);

            }
            catch (Exception)
            {
                return new ApiResult<Coupon>(false, HttpStatusCode.InternalServerError, ResponseMessage.UnSuccessfullOperation, null);
            }
        }

        public Task<ApiResult<Coupon>> Select(Coupon obj)
        {
            throw new NotImplementedException();
        }
    }
}
