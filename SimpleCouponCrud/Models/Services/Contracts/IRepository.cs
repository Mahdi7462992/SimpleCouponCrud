using SimpleCouponCrud.Frameworks;

namespace SimpleCouponCrud.Models.Services.Contracts
{
    public interface IRepository<T>
    {
        Task<ApiResult<T>> Select(T obj);
        Task<ApiResult<T>> Insert(T obj);
        Task<ApiResult<T>> Delete(T obj);
    }
}
