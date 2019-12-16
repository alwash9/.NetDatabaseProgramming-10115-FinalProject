using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace NorthwindDB_Console_Final.Logging
{
    public class NLogger
    {
        public NLog.Logger Logger { get; set; }

        //private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public NLogger()
        {
            Logger = NLog.LogManager.GetCurrentClassLogger();
        }
        public void Log (string type, string message)
        {
            if(type.ToUpper() == "INFO")
            {
                
                this.InfoLog(message);
            }
            
            else if(type.ToUpper() == "ERROR")
            {
                this.ErrorLog(message);
            }

            else if(type.ToUpper() == "WARN")
            {
                this.WarnLog(message);
            }
        }

        private void InfoLog (string message)
        {
            Console.WriteLine();
            Logger.Info(message);
            Console.WriteLine();
        }

        private void ErrorLog(string message)
        {
            Console.WriteLine();
            Logger.Error(message);
            Console.WriteLine();
        }

        private void WarnLog(string message)
        {
            Console.WriteLine();
            Logger.Warn(message);
            Console.WriteLine();
        }


    }
}
