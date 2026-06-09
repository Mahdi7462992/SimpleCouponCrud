using System.Net;

namespace SimpleCouponCrud.Frameworks
{
    public class ApiResult
    {
        public ApiResult(bool isSuccess, HttpStatusCode httpStatusCode, string message)
        {
            IsSuccess = isSuccess;
            HttpStatusCode = httpStatusCode;
            Message = message;
        }
        public bool IsSuccess { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
    }


    public class ApiResult<T>
    {
        public ApiResult(bool isSuccess, HttpStatusCode httpStatusCode, string message, T data)
        {
            IsSuccess = isSuccess;
            HttpStatusCode = httpStatusCode;
            Message = message;
            Data = data;
        }
        public bool IsSuccess { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
    }
}
