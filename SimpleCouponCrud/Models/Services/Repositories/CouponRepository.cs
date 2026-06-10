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
        public async Task<ApiResult> Delete(Guid id)
        {
            var coupon = await _context.Coupons.FindAsync(id);
            if (coupon is null)
            {
                return new ApiResult(false, HttpStatusCode.NotFound, ResponseMessage.NullInput);
            }

            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();
            return new ApiResult(true, HttpStatusCode.OK, ResponseMessage.SuccessfullOperation);
        }


        public async Task<Coupon?> GetByCode(string code)
        {
            var result = await _context.Coupons
                 .FirstOrDefaultAsync(x => x.Code == code);
            return result;
        }

        public async Task<ApiResult<Coupon>> Insert(Coupon obj)
        {
            if (obj is null)
            {
                return new ApiResult<Coupon>(false, HttpStatusCode.BadRequest, ResponseMessage.NullInput, null);
            }
            await _context.Coupons.AddAsync(obj);
            await _context.SaveChangesAsync();
            return new ApiResult<Coupon>(true, HttpStatusCode.Created, ResponseMessage.SuccessfullOperation, obj);
        }
    }
}
