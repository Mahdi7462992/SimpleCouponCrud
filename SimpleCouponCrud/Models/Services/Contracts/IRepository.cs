using SimpleCouponCrud.Frameworks;

namespace SimpleCouponCrud.Models.Services.Contracts
{
    public interface IRepository<T, TCollection>
    {
        Task<ApiResult<TCollection>> SelectAll();
        Task<ApiResult<T>> Select(T obj);
        Task<ApiResult<T>> Insert(T obj);
        Task<ApiResult<T>> Update(T obj);
        Task<ApiResult<T>> Delete(T obj);
    }
}
