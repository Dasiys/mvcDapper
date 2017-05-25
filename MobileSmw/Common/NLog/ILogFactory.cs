using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOC;
using NLog;

namespace Common.NLog
{
    public interface ILogFactory:IDependency
    {
        void Info(string functionName, string message = "", object data = null);

        void Error(string functionName, string message = "", object data = null);

        void Debug(string functionName, string message = "", object data = null);

        void Warn(string functionName, string message = "", object data = null);

        void Write(LogLevel logLevel,string functionName, string message = "", object data = null);
    }
}
