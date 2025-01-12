using System.Net;

namespace Ecommerce.Application.Bases
{
    public class Response<T>(HttpStatusCode statusCode = HttpStatusCode.OK, object meta = null, bool succeeded = true, string message = null, T data = default)
    {
        public HttpStatusCode StatusCode { get; set; } = statusCode;
        public object Meta { get; set; } = meta;
        public bool Succeeded { get; set; } = succeeded;
        public string Message { get; set; } = message;

        public T Data { get; set; } = data;

    }
}
