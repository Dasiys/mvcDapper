using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ioc;
using Newtonsoft.Json;
using NLog;

namespace Common.NLog
{
    public class LogFactory : ILogFactory
    {
        private readonly Logger _sqlLogger = LogManager.GetLogger("sql");
        private readonly Logger _httpLogger = LogManager.GetLogger("http");
        public void Debug(LogType logType, string functionName, string message = "", object data = null)
        {
            Write(LogLevel.Debug, logType, functionName, message, data);
        }

        public void Error(LogType logType, string functionName, string message = "", object data = null)
        {
            Write(LogLevel.Error, logType, functionName, message, data);
        }

        public void Info(LogType logType, string functionName, string message = "", object data = null)
        {
            Write(LogLevel.Info, logType, functionName, message, data);
        }

        public void Warn(LogType logType, string functionName, string message = "", object data = null)
        {
            Write(LogLevel.Warn, logType, functionName, message, data);
        }
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="logType"></param>
        /// <param name="functionName"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public void Write(LogLevel logLevel, LogType logType, string functionName, string message = "", object data = null)
        {
            var logger = logType == LogType.Http ? _httpLogger : _sqlLogger;
            var info = $"functionName:{functionName};message:{message};data:{JsonConvert.SerializeObject(data)}";
            logger.Log(logLevel, info);
        }
    }
}
