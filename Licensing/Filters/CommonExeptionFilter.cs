using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Licensing.Filters
{
    public class CommonExeptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var message = string.Empty;
            var status = HttpStatusCode.InternalServerError;

            var exceptionType = context.Exception.GetType();
            if(exceptionType == typeof(UnauthorizedAccessException))
            {
                message = "Unauthorized Access";
                status = HttpStatusCode.Unauthorized;
            }
            else if (exceptionType == typeof(NotImplementedException))
            {
                message = "A Server Error Accured";
                status = HttpStatusCode.NotImplemented;
            }
            else if (exceptionType == typeof(NotFoundResult))
            {
                message = "Not Found";
                status = HttpStatusCode.NotFound;
            }
            else if (exceptionType == typeof(NoContentResult))
            {
                message = "Not Content";
                status = HttpStatusCode.NoContent;
            }
            else if(exceptionType == typeof(FormatException))
            {
                message = "Not a Base64 String";
                status = HttpStatusCode.NoContent;
            }
            else
            {
                message = "Internal Sever Error";
                status = HttpStatusCode.InternalServerError;
            }
            var response = new HttpResponseMessage(status)
            {
                Content = new StringContent(message),
                ReasonPhrase = "from ExceptionFilter",
            };
            /*throw new NotImplementedException(message);*/
            context.Result = new OkObjectResult(new {err = true , Message = message});
        }
    }
}
