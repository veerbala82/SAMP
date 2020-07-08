using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Json;
using System.Net.Http.Formatting;
using SAMP.Models.Errors;

namespace SAMP.API.Common
{
    public class ValidateModelFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var modelState = actionContext.ModelState;
            if (!modelState.IsValid)
            {
                Errors Errors = new Errors
                {
                    Response = new Response() { EsErrors = new EsErrors() }
                };

                var errors = new List<string>();

                foreach (var state in modelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }

                Errors.Response.EsErrors.ErrorCode = 400;
                Errors.Response.EsErrors.ErrorDescription = string.Join(";", errors); ;
                Errors.Response.EsErrors.ErrorReference = string.Empty;

                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, Errors, JsonMediaTypeFormatter.DefaultMediaType);
            }
        }
    }
}