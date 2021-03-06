﻿using SAMP.API.Common;
using SAMP.BAL;
using SAMP.BAL.Interfaces;
using SAMP.Models.Common;
using SAMP.Models.SearchFilters;
using SAMP.Models.SOW;
using SAMP.Models.SystemParameters;
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
        /// IRemarksService object
        /// </summary>
        private readonly IRemarksService _remarksService;

        /// <summary>
        /// IRemarksService object
        /// </summary>
        private readonly ISystemParametersService _spService;

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
        public CommonController(IRemarksService remarksService, ISystemParametersService spService)
        {
            _remarksService = remarksService;
            _spService = spService;
            logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            logger.Info(string.Format("{0}.{1} - START", DateTime.Now, DateTime.Now.Day));
        }
        #endregion

        [HttpGet]
        [Route("RemarksDetails")]
        public RemarksRes GetRemarksDetails()
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

            RemarksRes objRes = _remarksService.GetRemarks(req);

            logger.Info(string.Format("{0}.{1} - END", this.GetType().Name, MethodBase.GetCurrentMethod().Name));

            return objRes;
        }

        [HttpGet]
        [Route("SystemParameters")]
        public SPRes GetSystemParameters()
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

            SPRes objRes = _spService.GetSystemParameters(req);

            logger.Info(string.Format("{0}.{1} - END", this.GetType().Name, MethodBase.GetCurrentMethod().Name));

            return objRes;
        }
    }
}
