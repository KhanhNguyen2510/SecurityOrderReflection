using System;

namespace SOR.Data.SystemBase
{
    public class ApiException : Exception
    {
        public ApiException(string message, object result, int? statusCode = 500) : base(message)
        {
            StatusCode = statusCode ?? 500;
            Message = message;
            Result = result;
        }

        public ApiException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public ApiException(string message) : base(message)
        {
            StatusCode = 500;
            Message = message;
        }

        public ApiException() : base("Vui lòng thử lại sau")
        {
        }

        public int StatusCode { get; set; } = 500;
        public override string Message { get; } = "Vui lòng thử lại sau";
        public object Result { get; set; }
    }
}
