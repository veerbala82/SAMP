using SAMP.API.TokenManagement;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            try
            {
                if (actionContext.Request.Headers.GetValues("Authorization") != null)
                {
                    // Get Authorization value from header
                    string authenticationToken = actionContext.Request.Headers.GetValues("Authorization").FirstOrDefault();

                    string userName = TokenManager.ValidateToken(authenticationToken);

                    if (string.IsNullOrEmpty(userName))
                    {
                        actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                        return;
                    }

                    HttpContext.Current.Response.AddHeader("AuthenticationToken", authenticationToken);
                    return;
                }
                else
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                    return;
                }
            }
            catch (Exception)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                return;
            }
            
        }

        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            Contract.Assert(actionContext != null);

            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                       || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }
    }
}