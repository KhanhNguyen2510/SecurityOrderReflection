namespace SOR.Data.SystemBase
{
    public  class ApiResponse
    {
        public ApiResponse(string message, object result, int statusCode = 200)
        {
            StatusCode = statusCode;
            Message = message;
            Result = result;
        }

        public ApiResponse(string message, int statusCode)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public ApiResponse(object result)
        {
            Result = result;
        }

        public ApiResponse(string message)
        {
            Message = message;
        }

        public int StatusCode { get; set; } = 200;
        public string Message { get; set; } = "Thành công";
        public object Result { get; set; }
    }
}
