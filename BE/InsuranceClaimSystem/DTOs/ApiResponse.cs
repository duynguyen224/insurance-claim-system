namespace InsuranceClaimSystem.DTOs
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; private set; }
        public string Message { get; private set; }
        public Dictionary<string, List<string>> Errors { get; private set; }
        public T Data { get; private set; }
        public bool IsSuccess { get; private set; }

        private ApiResponse()
        {
            // Default constructor sets default values
            StatusCode = default;
            Message = string.Empty;
            Errors = new Dictionary<string, List<string>>();
            Data = default;
            IsSuccess = false;
        }

        public class Builder
        {
            private readonly ApiResponse<T> _response;

            public Builder()
            {
                _response = new ApiResponse<T>();
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

            public Builder SetErrors(Dictionary<string, List<string>> errors)
            {
                _response.Errors = errors;
                return this;
            }

            public Builder SetData(T data)
            {
                _response.Data = data;
                return this;
            }

            public Builder SetIsSuccess(bool isSuccess)
            {
                _response.IsSuccess = isSuccess;
                return this;
            }

            public ApiResponse<T> Build()
            {
                return _response;
            }
        }
    }
}
