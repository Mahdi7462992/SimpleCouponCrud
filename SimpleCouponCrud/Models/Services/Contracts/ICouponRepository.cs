using SimpleCouponCrud.Models.Entities;

namespace SimpleCouponCrud.Models.Services.Contracts
{
    public interface ICouponRepository : IRepository<Coupon>
    {
        Task<Coupon?> GetByCode(string code);
    }
}
