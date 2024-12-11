namespace Lab6.Models
{
    public class ApiResponse<T>
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public List<T> Data { get; set; } = new();

        public ApiResponse(string message, int statusCode, List<T> data = null)
        {
            Message = message;
            StatusCode = statusCode;
            Data = data ?? new List<T>();
        }
    }
}
