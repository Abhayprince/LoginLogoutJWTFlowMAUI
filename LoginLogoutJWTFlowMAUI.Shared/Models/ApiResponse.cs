using System.Net;

namespace LoginLogoutJWTFlowMAUI.Shared.Models
{
    public sealed class ApiResponse<TData>
    {
        public ApiResponse(TData data = default!) => Data = data;

        public bool Status { get; set; } = true;
        public IEnumerable<string>? Errors { get; set; }
        public int StatusCode { get; set; } = 200;
        public TData Data { get; set; }

        public static ApiResponse<TData> Success(int statusCode, TData data = default!) =>
            new(data)
            {
                StatusCode = statusCode,
                Status = true
            };

        public static ApiResponse<TData> Success(TData data = default!, HttpStatusCode statusCode = HttpStatusCode.OK) => Success((int)statusCode, data);

        public static ApiResponse<TData> Success(int statusCode) =>
            new()
            {
                StatusCode = statusCode,
                Status = true
            };

        public static ApiResponse<TData> Success(HttpStatusCode statusCode = HttpStatusCode.OK) => Success((int)statusCode);

        public static ApiResponse<TData> Failure(IEnumerable<string> errors, int statusCode) =>
            new(default!)
            {
                StatusCode = statusCode,
                Status = false,
                Errors = errors
            };
        public static ApiResponse<TData> Failure(IEnumerable<string> errors, HttpStatusCode statusCode) => Failure(errors, (int)statusCode);

        public static ApiResponse<TData> Failure(string errors, int statusCode) => Failure(new List<string> { errors }, statusCode);
        public static ApiResponse<TData> Failure(string errors, HttpStatusCode statusCode) => Failure(new List<string> { errors }, (int)statusCode);
    }
}
