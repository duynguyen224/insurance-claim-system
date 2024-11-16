namespace InsuranceClaimSystem.DTOs
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; private set; }
        public int StatusCode { get; private set; }
        public string Message { get; private set; }
        public T Data { get; private set; }
        public Dictionary<string, List<string>> Errors { get; private set; }

        private ApiResponse()
        {
            // Default constructor sets default values
            IsSuccess = false;
            StatusCode = default;
            Message = string.Empty;
            Data = default;
            Errors = new Dictionary<string, List<string>>();
        }

        public class Builder
        {
            private readonly ApiResponse<T> _response;

            public Builder()
            {
                _response = new ApiResponse<T>();
            }

            public Builder SetIsSuccess(bool isSuccess)
            {
                _response.IsSuccess = isSuccess;
                return this;
            }

            public Builder SetStatusCode(int statusCode)
            {
                _response.StatusCode = statusCode;
                return this;
            }

            public Builder SetMessage(string message)
            {
                _response.Message = message;
                return this;
            }

            public Builder SetData(T data)
            {
                _response.Data = data;
                return this;
            }

            public Builder SetErrors(Dictionary<string, List<string>> errors)
            {
                _response.Errors = errors;
                return this;
            }

            public ApiResponse<T> Build()
            {
                return _response;
            }
        }

        // Helper methods
        public static ApiResponse<T> BuildResponse(int statusCode, string message, T data)
        {
            return new ApiResponse<T>.Builder()
                .SetIsSuccess(true)
                .SetStatusCode(statusCode)
                .SetMessage(message)
                .SetData(data)
                .Build();
        }

        public static ApiResponse<T> BuildErrorResponse(int statusCode, string message, Dictionary<string, List<string>> errors = default)
        {
            return new ApiResponse<T>.Builder()
                .SetIsSuccess(false)
                .SetStatusCode(statusCode)
                .SetMessage(message)
                .SetErrors(errors)
                .Build();
        }
    }
}
