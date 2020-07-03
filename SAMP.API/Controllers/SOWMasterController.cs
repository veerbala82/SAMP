using SAMP.API.Common;
using SAMP.BAL;
using SAMP.Models.SOW;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Description;

namespace SAMP.API.Controllers
{
    [CustomAuthorize]
    [RoutePrefix("Api/SOWMaster")]
    public class SOWMasterController : ApiController
    {
        #region Private Members
        /// <summary>
        /// The Logger
        /// </summary>
        private static log4net.ILog logger = null;

        /// <summary>
        /// ICustomerDeliveryService object
        /// </summary>
        private readonly ISOWMasterService _sowMasterService;

        /// <summary>
        /// User
        /// </summary>
        private string user = string.Empty;
        #endregion

        #region Constructor
        /// <summary>
        /// LoginController Constructor
        /// </summary>
        /// <param name=""></param>
        public SOWMasterController(ISOWMasterService sowMasterService)
        {
            this._sowMasterService = sowMasterService;
            logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            logger.Info(string.Format("{0}.{1} - START", DateTime.Now, DateTime.Now.Day));
        }
        #endregion

        [HttpGet]
        public SOWSaveRes GetSOWs(SOWReq req)
        {
            logger.Info(Environment.NewLine + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + Environment.NewLine);

            logger.Info(string.Format("{0}.{1} - START", this.GetType().Name, MethodBase.GetCurrentMethod().Name));

            SOWSaveRes objRes = _sowMasterService.InsertSOWMaster(req, user);

            return objRes;
        }

        [HttpPost]
        [ResponseType(typeof(SOWSaveRes))]        
        [Route("SOWInsert")]
        public IHttpActionResult Post(SOWReq req)
        {
            logger.Info(Environment.NewLine + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + Environment.NewLine);

            logger.Info(string.Format("{0}.{1} - START", this.GetType().Name, MethodBase.GetCurrentMethod().Name));

            SOWSaveRes objRes = new SOWSaveRes();

            var requestHeader = Request;

            var headers = requestHeader.Headers;

            if (headers.Contains("UID"))
            {
                var UID = headers.GetValues("UID").First();
                this.user = EncryptDecrypt.Decrypt(UID);
            }

            objRes = _sowMasterService.InsertSOWMaster(req, user);

            logger.Info(string.Format("{0}.{1} - END", this.GetType().Name, MethodBase.GetCurrentMethod().Name));

            return this.Ok(objRes);
        }

        [HttpPut]
        public SOWSaveRes Put(SOWReq req)
        {
            logger.Info(Environment.NewLine + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + Environment.NewLine);

            logger.Info(string.Format("{0}.{1} - START", this.GetType().Name, MethodBase.GetCurrentMethod().Name));

            var requestHeader = Request;

            var headers = requestHeader.Headers;

            if (headers.Contains("UID"))
            {
                var UID = headers.GetValues("UID").First();
                this.user = EncryptDecrypt.Decrypt(UID);
            }

            SOWSaveRes objRes = null;
            //SOWSaveRes objRes = _sowMasterService.InsertSOWMaster(req);

            return objRes;
        }
    }
}
