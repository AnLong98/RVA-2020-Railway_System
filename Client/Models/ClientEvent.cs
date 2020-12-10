using Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class ClientEvent
    {
        public ClientEvent(LogType logType, DateTime eventTime, string message)
        {
            LogType = logType;
            EventTime = eventTime;
            Message = message;
        }

        public LogType LogType { get; set; }
        public DateTime EventTime { get; set; }
        public string Message { get; set; }


    }
}
