namespace Stock_Back.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public T Data { get; private set; }

        private ApiResponse(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public static ApiResponse<T> SuccessResponse(T data, string message = "")
            => new ApiResponse<T>(true, message, data);

        public static ApiResponse<T> NotFoundResponse(string message)
            => new ApiResponse<T>(false, message, default);

        public static ApiResponse<T> ErrorResponse(string message)
            => new ApiResponse<T>(false, message, default);

        public static ApiResponse<T> BadRequest(T data, string message)
            => new ApiResponse<T>(false, message, data);
    }

}

