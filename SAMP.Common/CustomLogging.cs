using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
// https://denhamcoder.net/2018/12/21/integrate-log4net-into-an-asp-net-web-api-project/

namespace SAMP.Common
{
    public static class CustomLogging
    {
        private static ILog _log = null;
        private static string _logFile = null;

        public static void Initialize(string ApplicationPath)
        {
            _logFile = Path.Combine(ApplicationPath, "App_Data", "Demo.UI.WebService.log");
            GlobalContext.Properties["LogFileName"] = _logFile;

            log4net.Config.XmlConfigurator.Configure(new FileInfo(Path.Combine(ApplicationPath, "Log4Net.config")));

            _log = LogManager.GetLogger("Demo.UI");
        }

        public static string LogFile
        {
            get { return _logFile; }
        }
    }
}
