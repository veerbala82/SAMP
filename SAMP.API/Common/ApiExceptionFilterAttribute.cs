using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http.Filters;
using SAMP.Models.Errors;

namespace SAMP.API.Common
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var exceptionType = context.Exception.GetType().ToString();
            var exception = context.Exception.InnerException ?? context.Exception;

            HttpResponseMessage httpResponseMessage;

            httpResponseMessage = CreateHttpResponseMessage(context.Request,
                                HttpStatusCode.InternalServerError, "An error occurred, please try again. If error persists contact support.", 500);

            context.Response = httpResponseMessage;
        }

        public HttpResponseMessage CreateHttpResponseMessage(HttpRequestMessage request, HttpStatusCode statusCode, string error, int resStatusCode)
        {
            Errors Errors = new Errors
            {
                Response = new Response() { EsErrors = new EsErrors() }
            };

            Errors.Response.EsErrors.ErrorCode = resStatusCode;
            Errors.Response.EsErrors.ErrorDescription = error;
            Errors.Response.EsErrors.ErrorReference = "Check Log File.";

            return request.CreateResponse(statusCode, Errors, JsonMediaTypeFormatter.DefaultMediaType);
        }
    }
}