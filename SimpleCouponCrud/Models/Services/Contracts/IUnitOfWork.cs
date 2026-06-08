namespace SimpleCouponCrud.Models.Services.Contracts
{
    public interface IUnitOfWork
    {
        ICouponRepository Coupons { get; }
        Task<int> SaveChangesAsync();
    }
}
