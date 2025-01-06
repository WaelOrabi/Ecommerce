using System.Net;

namespace Ecommerce.Application.Bases
{
    public class Response<T>(HttpStatusCode statusCode = HttpStatusCode.OK, object meta = null, bool succeeded = true, string message = null, T data = default)
    {
        public HttpStatusCode StatusCode { get; set; } = statusCode;
        public object Meta { get; set; } = meta;
        public bool Succeeded { get; set; } = succeeded;
        public string Message { get; set; } = message;
        public List<string> Errors { get; set; }
        public T Data { get; set; } = data;
        public void AddError(string error)
        {
            Errors ??= new List<string>();
            Errors.Add(error);
        }
    }
}
