using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace SAMP.API.Common
{
    public class Log4NetExceptionLogger: ExceptionLogger
    {
        private ILog log = LogManager.GetLogger(typeof(Log4NetExceptionLogger));

        public async override Task LogAsync(ExceptionLoggerContext context, System.Threading.CancellationToken cancellationToken)
        {
            log.Info(string.Format("{0}.{1} - ERROR START", DateTime.Now, DateTime.Now.Day));
            log.Error("An unhandled exception occurred.", context.Exception);            
            log.Info(string.Format("{0}.{1} - ERROR END", DateTime.Now, DateTime.Now.Day));
            await base.LogAsync(context, cancellationToken);
        }

        //public override void Log(ExceptionLoggerContext context)
        //{            
        //    log.Error("An unhandled exception occurred.", context.Exception);
        //    base.Log(context);
        //}

        public override bool ShouldLog(ExceptionLoggerContext context)
        {
            return base.ShouldLog(context);
        }
    }
}