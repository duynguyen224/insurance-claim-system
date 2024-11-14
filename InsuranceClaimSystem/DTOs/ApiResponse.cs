using InsuranceClaimSystem.Constants;

namespace InsuranceClaimSystem.DTOs
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public Dictionary<string, List<string>> Errors { get; set; }

        public static ApiResponse<T> Success(T data, int statusCode = (int)HttpStatus.Ok, string message = null)
        {
            return new ApiResponse<T>
            {
                IsSuccess = true,
                StatusCode = statusCode,
                Data = data,
                Message = message,
                Errors = null
            };
        }

        public static ApiResponse<T> Failure(Dictionary<string, List<string>> errors, int statusCode = (int)HttpStatus.BadRequest, string message = null)
        {
            return new ApiResponse<T>
            {
                IsSuccess = false,
                StatusCode = statusCode,
                Data = default,
                Message = message,
                Errors = errors
            };
        }
    }
}
