using SimpleCouponCrud.Frameworks;

namespace SimpleCouponCrud.Models.Services.Contracts
{
    public interface IRepository<T>
    {
        Task<T> GetById(Guid id);
        Task<ApiResult<T>> Insert(T obj);
        Task<ApiResult> Delete(Guid id);
    }
}
