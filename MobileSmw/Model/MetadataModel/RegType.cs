using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Model.MetadataModel
{
    /// <summary>
    /// 注册类型
    /// </summary>
    public enum RegType
    {
        /// <summary>
        /// 全部
        /// </summary>
        All = -1,
        /// <summary>
        /// APP
        /// </summary>
        APP = 0,
        /// <summary>
        /// PC
        /// </summary>
        PC = 1,
        /// <summary>
        /// Own
        /// </summary>
        Own = 2
    }
}
