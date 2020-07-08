using SAMP.API.Common;
using SAMP.BAL;
using SAMP.Models.Common;
using SAMP.Models.SearchFilters;
using SAMP.Models.SOW;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Description;

namespace SAMP.API.Controllers
{
    [CustomAuthorize]
    [RoutePrefix("Api/Common")]
    public class CommonController : ApiController
    {
        #region Private Members
        /// <summary>
        /// The Logger
        /// </summary>
        private static log4net.ILog logger = null;

        /// <summary>
        /// ICustomerDeliveryService object
        /// </summary>
        private readonly IRemarksService _remarksService;

        /// <summary>
        /// User
        /// </summary>
        private string user = string.Empty;
        #endregion

        #region Constructor
        /// <summary>
        /// CommonController Constructor
        /// </summary>
        /// <param name=""></param>
        public CommonController(IRemarksService remarksService)
        {
            this._remarksService = remarksService;
            logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            logger.Info(string.Format("{0}.{1} - START", DateTime.Now, DateTime.Now.Day));
        }
        #endregion

        [HttpGet]
        [Route("RemarksDetails")]
        public RemarksRes GetRemarksDetails(RemarksReq req)
        {
            logger.Info(Environment.NewLine + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + Environment.NewLine);

            logger.Info(string.Format("{0}.{1} - START", this.GetType().Name, MethodBase.GetCurrentMethod().Name));

            RemarksRes objRes = _remarksService.GetRemarks(req);          

            return objRes;
        }
    }
}
