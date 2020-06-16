using SAMP.API.Common;
using SAMP.API.TokenManagement;
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
    [Authorize]
    [RoutePrefix("Api/Login")]
    public class LoginController : ApiController
    {
        #region Private Members
        /// <summary>
        /// The Logger
        /// </summary>
        private static log4net.ILog logger = null;
        #endregion

        #region Constructor
        /// <summary>
        /// LoginController Constructor
        /// </summary>
        /// <param name=""></param>
        public LoginController()
        {
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

        [AllowAnonymous]
        [HttpPost]
        [Route("UserLogin")]        
        public LoginRes UserLogin(LoginReq req)
        {
            logger.Info(Environment.NewLine + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + Environment.NewLine);

            logger.Info(string.Format("{0}.{1} - START", this.GetType().Name, MethodBase.GetCurrentMethod().Name));

            return new LoginRes { Status = "Success", Message = TokenManager.GenerateToken(req.Email) };
        }

        // GET: api/Login
        [HttpGet]
        [Route("TestToken")]
        public IEnumerable<string> TestToken()
        {
            logger.Info(Environment.NewLine + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + Environment.NewLine);

            logger.Info(string.Format("{0}.{1} - START", this.GetType().Name, MethodBase.GetCurrentMethod().Name));

            return new string[] { "value1", "value2" };
        }

        [Route("Validate")]
        [HttpGet]
        public LoginRes Validate(string token, string username)
        {
            //int UserId = new UserRepository().GetUser(username);
            //if (UserId == 0) return new LoginRes { Status = "Invalid", Message = "Invalid User." };
            string tokenUsername = TokenManager.ValidateToken(token);
            if (username.Equals(tokenUsername))
            {
                return new LoginRes
                {
                    Status = "Success",
                    Message = "OK",
                };
            }
            return new LoginRes { Status = "Invalid", Message = "Invalid Token." };
        }


        // GET: api/Login/5
        [Route("Get2")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Login
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
    }
}
