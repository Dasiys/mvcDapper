using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ioc;
using NLog;

namespace Common.NLog
{
    /// <summary>
    /// 日志
    /// </summary>
    public interface ILogFactory
    {
        void Info(LogType logType, string functionName,string message="", object data = null);

        void Error(LogType logType, string functionName,string message="", object data = null);

        void Debug(LogType logType, string functionName,string message="", object data = null);

        void Warn(LogType logType, string functionName,string message="", object data = null);

        void Write(LogLevel logLevel, LogType logType, string functionName, string message = "", object data = null);
    }
}
