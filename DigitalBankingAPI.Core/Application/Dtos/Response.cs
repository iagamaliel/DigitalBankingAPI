
namespace DigitalBankingAPI.Core.Application.Dtos
{
    public class Response<T>
    {
        public bool Succeeded { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public int StatusCode { get; set; }

        public Response() { }

        public Response(T data, string message = null)
        {
            Succeeded = true;
            Data = data;
            Message = message;
            StatusCode = 200;
        }

        public Response(string message, int statusCode = 400)
        {
            Succeeded = false;
            Message = message;
            StatusCode = statusCode;
        }

        public static Response<T> Success(T data, string message = null)
        {
            return new Response<T>(data, message);
        }

        public static Response<T> Fail(string message, int statusCode = 400)
        {
            return new Response<T>
            {
                Succeeded = false,
                Message = message,
                StatusCode = statusCode
            };
        }
    }
}
