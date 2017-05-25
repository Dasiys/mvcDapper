using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NLog;

namespace Common.NLog
{
    public class LogFactory:ILogFactory
    {
        /// <summary>
        /// 日志记录信息
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public void Info(string functionName, string message = "", object data = null)
        {
            Write(LogLevel.Info, functionName,message,data);
        }

        /// <summary>
        /// 日志记录错误信息
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public void Error(string functionName, string message = "", object data = null)
        {
           Write(LogLevel.Error, functionName,message,data);
        }

        /// <summary>
        /// 日志记录Debug时的信息
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public void Debug(string functionName, string message = "", object data = null)
        {
           Write(LogLevel.Debug, functionName,message,data);
        }

        /// <summary>
        /// 日志记录警告信息
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public void Warn(string functionName, string message = "", object data = null)
        {
           Write(LogLevel.Warn, functionName,message,data);
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="functionName"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public void Write(LogLevel logLevel, string functionName, string message = "", object data = null)
        {
            var logger = LogManager.GetLogger("logger");
            var info = $"FunctionName:{functionName};Message:{message};Data:{JsonConvert.SerializeObject(data)}";
            logger.Log(logLevel,info);
        }
    }
}
