using SAMP.API.Common;
using SAMP.BAL;
using SAMP.BAL.Interfaces;
using SAMP.Models.AccountMaster;
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
    [RoutePrefix("Api/AccountMaster")]
    public class AccountMasterController : ApiController
    {
        #region Private Members
        /// <summary>
        /// The Logger
        /// </summary>
        private static log4net.ILog logger = null;

        /// <summary>
        /// ICustomerDeliveryService object
        /// </summary>
        private readonly IAccountMasterService _aMService;

        /// <summary>
        /// User
        /// </summary>
        private string user = string.Empty;

        /// <summary>
        /// searchFilters
        /// </summary>
        private string searchFilters = string.Empty;
        #endregion

        #region Constructor
        /// <summary>
        /// CommonController Constructor
        /// </summary>
        /// <param name=""></param>
        public AccountMasterController(IAccountMasterService aMService)
        {
            this._aMService = aMService;
            logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            logger.Info(string.Format("{0}.{1} - START", DateTime.Now, DateTime.Now.Day));
        }
        #endregion

        [HttpGet]
        //[Route("AccountMaster")]
        public AMRes GetAccountMasterDetails()
        {
            logger.Info(Environment.NewLine + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + Environment.NewLine);

            logger.Info(string.Format("{0}.{1} - START", this.GetType().Name, MethodBase.GetCurrentMethod().Name));

            var requestHeader = Request;

            var headers = requestHeader.Headers;

            if (headers.Contains("esFilters"))
            {
                searchFilters = headers.GetValues("esFilters").First();
            }

            SearchFiltersReq req = Newtonsoft.Json.JsonConvert.DeserializeObject<SearchFiltersReq>(searchFilters);

            AMRes objRes = _aMService.GetAccountMaster(req);

            logger.Info(string.Format("{0}.{1} - END", this.GetType().Name, MethodBase.GetCurrentMethod().Name));

            return objRes;
        }
    }
}
