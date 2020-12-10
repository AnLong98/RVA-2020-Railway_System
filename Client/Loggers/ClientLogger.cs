using Client.Managers;
using Client.Models;
using Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Loggers
{
    public class ClientLogger : ILogging
    {
        private static ClientLogger instance;
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static ClientEventManager eventManager = ClientEventManager.GetOrCreate();

        public static ClientLogger GetOrCreate()
        {

            if (instance == null) instance = new ClientLogger();

            return instance;
        }

        /// 
        /// <param name="message"></param>
        /// <param name="logType"></param>
        public void LogNewMessage(string message, LogType logType)
        {
            ClientEvent clientEvent = new ClientEvent(logType, DateTime.Now, message);
            switch (logType)
            {
                case LogType.INFO:
                    log.Info(message);
                    break;
                case LogType.DEBUG:
                    log.Debug(message);
                    break;
                case LogType.ERROR:
                    log.Error(message);
                    break;
                case LogType.WARNING:
                    log.Warn(message);
                    break;
            }

            eventManager.AddEvent(clientEvent);

        }
    }
}
