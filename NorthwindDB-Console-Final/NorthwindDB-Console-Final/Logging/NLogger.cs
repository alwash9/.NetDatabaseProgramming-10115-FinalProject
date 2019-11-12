using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace NorthwindDB_Console_Final.Logging
{
    class NLogger
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public void Log (string message, string type)
        {
            if(type.ToUpper() == "INFO")
            {
                this.InfoLog(message);
            }
            
            if(type.ToUpper() == "ERROR")
            {
                this.ErrorLog(message);
            }

            if(type.ToUpper() == "WARN")
            {
                this.WarnLog(message);
            }
        }

        private void InfoLog (string message)
        {
            logger.Info(message);
        }

        private void ErrorLog(string message)
        {
            logger.Error(message);
        }

        private void WarnLog(string message)
        {
            logger.Warn(message);
        }


    }
}
