using Ecommerce.Application.Resources;
using Microsoft.Extensions.Localization;
using System.Net;

namespace Ecommerce.Application.Bases
{
    public class ResponseHandler
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        public ResponseHandler(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }
        private Response<T> CreateResponse<T>(
            HttpStatusCode statusCode,
            bool succeeded,
            string message,
            T data = default,
            object meta = null
            )
        {
            return new Response<T>
            {
                StatusCode = statusCode,
                Succeeded = succeeded,
                Message = message,
                Data = data,
                Meta = meta
            };
        }
        public Response<T> GenerateDeleteResponse<T>(T data, string message = null)
        {
            return CreateResponse<T>(HttpStatusCode.OK, true, message ?? _stringLocalizer[SharedResourcesKeys.Deleted], data);
        }
        public Response<T> GenerateSuccessResponse<T>(T entity, object meta = null)
        {
            return CreateResponse(HttpStatusCode.OK, true, _stringLocalizer[SharedResourcesKeys.Success], entity, meta);
        }
        public Response<T> GenerateUnauthorizedResponse<T>()
        {
            return CreateResponse<T>(HttpStatusCode.Unauthorized, false, "Unauthorized");
        }


        public Response<T> GenerateBadRequestResponse<T>(string message = null)
        {
            return CreateResponse<T>(HttpStatusCode.BadRequest, false, message ?? "Bad Request");
        }
        public Response<T> GenerateInternalServerErrorResponse<T>(string message = null)
        {
            return CreateResponse<T>(HttpStatusCode.InternalServerError, false, message ?? "Error in Server");
        }
        public Response<T> GenerateUnprocessableEntityResponse<T>(string message = null)
        {
            return CreateResponse<T>(HttpStatusCode.UnprocessableEntity, false, message ?? "Unprocessable Entity");
        }
        public Response<T> GenerateNotFoundResponse<T>(string message = null)
        {
            return CreateResponse<T>(HttpStatusCode.NotFound, false, message ?? _stringLocalizer[SharedResourcesKeys.NotFound]);
        }
        public Response<T> GenerateCreatedResponse<T>(T entity, object meta = null)
        {
            return CreateResponse(HttpStatusCode.Created, true, _stringLocalizer[SharedResourcesKeys.Created], entity, meta);
        }
    }
}
