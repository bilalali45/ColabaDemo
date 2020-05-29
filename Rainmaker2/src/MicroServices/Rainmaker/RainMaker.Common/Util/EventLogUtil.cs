//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RainMaker.Common.Util
//{
//    public static class EventLogUtil
//    {
//        public static void WriteEventLog(EventLog eventlog , string message,System.Diagnostics.EventLogEntryType logType,bool isLogged=true)
//        {
//            try
//            {
//                if (eventlog!=null && isLogged)
//                    eventlog.WriteEntry(message, logType);
//            }
//            catch (Exception)
//            {
//            }
            
//        }

//        public static void CreateEventSource(string sourceName, string logName)
//        {
//            try
//            {
//                if (!System.Diagnostics.EventLog.SourceExists(sourceName))
//                {
//                    System.Diagnostics.EventLog.CreateEventSource(sourceName, logName);
//                }
//            }
//            catch (Exception)
//            {
//            }

//        }
//    }
//}
