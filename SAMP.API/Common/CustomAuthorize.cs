using SAMP.API.TokenManagement;
using SAMP.Models.Errors;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SAMP.API.Common
{
    public class CustomAuthorize : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (SkipAuthorization(actionContext)) return;

            base.OnAuthorization(actionContext);

            Errors Errors = new Errors
            {
                Response = new Response() { EsErrors = new EsErrors() }
            };

            try
            {
                if (actionContext.Request.Headers.GetValues("Authorization") != null)
                {
                    // Get Authorization value from header
                    string authenticationToken = actionContext.Request.Headers.GetValues("Authorization").FirstOrDefault();

                    string userName = TokenManager.ValidateToken(authenticationToken);

                    if (string.IsNullOrEmpty(userName))
                    {
                        Errors.Response.EsErrors.ErrorCode = 401;
                        Errors.Response.EsErrors.ErrorDescription = "Unauthorized access.";
                        Errors.Response.EsErrors.ErrorReference = string.Empty;
                        
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, Errors, JsonMediaTypeFormatter.DefaultMediaType);
                        return;
                    }
                    
                    HttpContext.Current.Response.AddHeader("AuthenticationToken", authenticationToken);
                    return;
                }
                else
                {                    
                    Errors.Response.EsErrors.ErrorCode = 401;
                    Errors.Response.EsErrors.ErrorDescription = "Unauthorized access. Authorization token not available.";
                    Errors.Response.EsErrors.ErrorReference = string.Empty;

                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, Errors, JsonMediaTypeFormatter.DefaultMediaType);
                    return;
                }
            }
            catch (Exception ex)
            {
                Errors.Response.EsErrors.ErrorCode = 400;
                Errors.Response.EsErrors.ErrorDescription = "Bad request. Contact system administrator";
                Errors.Response.EsErrors.ErrorReference = ex.Message;

                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, Errors, JsonMediaTypeFormatter.DefaultMediaType);                
                return;
            }

        }

        //This method is to skip methods with [AllowAnonymous] attribute
        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            Contract.Assert(actionContext != null);

            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                       || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }
    }
}