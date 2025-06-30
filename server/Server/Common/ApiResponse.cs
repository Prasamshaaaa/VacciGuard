namespace Server.Common
{
    public class ApiResponse<T>
    {

        public T Results { get; set; }
        public string Status { get; set; }
        public string ErrorMessage {  get; set; }

        public ApiResponse()
        {
            Status = string.Empty;
            ErrorMessage = string.Empty;
        }

        public static ApiResponse<T> FormatResult(T result)
        {
            return new ApiResponse<T> { Results = result };
        }

        public static ApiResponse<T> FormatResult(T results, string status)
        {
            return new ApiResponse<T> { Results = results, Status = status };
        }

        public static ApiResponse<T> FormatResult(T results, string status, string errorMessage)
        {
            return new ApiResponse<T> { Results = results, Status = status, ErrorMessage = errorMessage };
        }

    }


}
