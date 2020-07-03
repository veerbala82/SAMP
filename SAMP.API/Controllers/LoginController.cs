using SAMP.API.Common;
using SAMP.API.TokenManagement;
using SAMP.BAL;
using SAMP.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace SAMP.API.Controllers
{
    [CustomAuthorize]
    [RoutePrefix("Api/Login")]  
    public class LoginController : ApiController
    {
        #region Private Members
        /// <summary>
        /// The Logger
        /// </summary>
        private static log4net.ILog logger = null;

        /// <summary>
        /// ICustomerDeliveryService object
        /// </summary>
        private readonly ILoginService _loginService;
        #endregion

        #region Constructor
        /// <summary>
        /// LoginController Constructor
        /// </summary>
        /// <param name=""></param>
        public LoginController(ILoginService loginService)
        {
            this._loginService = loginService;
            logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            logger.Info(string.Format("{0}.{1} - START", DateTime.Now, DateTime.Now.Day));
        }
        #endregion

        // GET: api/Login        
        [HttpGet]
        [Route("Get")]
        public IEnumerable<string> Get()
        {
            logger.Info(Environment.NewLine + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + Environment.NewLine);

            logger.Info(string.Format("{0}.{1} - START", this.GetType().Name, MethodBase.GetCurrentMethod().Name));

            return new string[] { "value1", "value2" };
        }

        //[AllowAnonymous]
        //[HttpPost]
        //[Route("UserLogin")]
        //public LoginRes UserLogin(LoginReq req)
        //{
        //    logger.Info(Environment.NewLine + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + Environment.NewLine);

        //    logger.Info(string.Format("{0}.{1} - START", this.GetType().Name, MethodBase.GetCurrentMethod().Name));

        //    return new LoginRes { Status = "Success", Message = TokenManager.GenerateToken(req.Email) };
        //}

        [AllowAnonymous]
        [HttpPost]
        [Route("UserLogin")]
        public LoginRes UserLogin(LoginReq req)
        {
            logger.Info(Environment.NewLine + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + Environment.NewLine);

            logger.Info(string.Format("{0}.{1} - START", this.GetType().Name, MethodBase.GetCurrentMethod().Name));            

            LoginRes objRes = _loginService.ValidateLoginCredentials(req);

            if(objRes.ValidUser == 1)
            {
                objRes.Token = TokenManager.GenerateToken(objRes.Email);
                objRes.UID = EncryptDecrypt.Encrypt(objRes.Email);
            }

            return objRes;
        }

    }
}
