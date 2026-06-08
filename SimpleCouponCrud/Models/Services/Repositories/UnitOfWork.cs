using SimpleCouponCrud.Models.Services.Contracts;

namespace SimpleCouponCrud.Models.Services.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjectDbContext _context;
        public ICouponRepository Coupons { get; }
        public UnitOfWork(ProjectDbContext context, ICouponRepository couponRepository)
        {
            _context = context;
            Coupons = couponRepository;
        }

        public Task<int> SaveChangesAsync()
         => _context.SaveChangesAsync();
    }
}
