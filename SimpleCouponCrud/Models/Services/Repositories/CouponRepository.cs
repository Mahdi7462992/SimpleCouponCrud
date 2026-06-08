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
                    return new ApiResult<Coupon>(false, HttpStatusCode.UnprocessableContent, ResponseMessage.NullInput, null);
                }

                var person = await _context.Coupon.FindAsync(obj.Id);
                if (person == null)
                {
                    return new ApiResult<Coupon>(false, HttpStatusCode.NotFound, ResponseMessage.NullInput, null);
                }

                _context.Coupon.Remove(person);
                await _context.SaveChangesAsync();
                return new ApiResult<Coupon>(true, HttpStatusCode.OK, ResponseMessage.SuccessfullOperation, person);
            }
            catch (Exception)
            {
                return new ApiResult<Coupon>(false, HttpStatusCode.InternalServerError, ResponseMessage.UnSuccessfullOperation, null);
            }
        }

        public async Task<ApiResult<Coupon>> Insert(Coupon obj)
        {
            try
            {
                if (obj is null)
                {
                    return new ApiResult<Coupon>(false, HttpStatusCode.UnprocessableContent, ResponseMessage.NullInput, null);
                }
                await _context.AddAsync(obj);
                await _context.SaveChangesAsync();
                return new ApiResult<Coupon>(true, HttpStatusCode.OK, ResponseMessage.SuccessfullOperation, obj);

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
