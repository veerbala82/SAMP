using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SAMP.API.Common
{
    public class Bootstrapper
    {
        /// <summary>
		/// Runs this instance.
		/// </summary>
		public static void Run()
        {
            AutofacWebapiConfig.Initialize(GlobalConfiguration.Configuration);
        }
    }
}