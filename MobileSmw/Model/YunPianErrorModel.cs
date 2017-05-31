using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 云片错误模型
    /// </summary>
    public class YunPianErrorModel
    {
        /// <summary>
        /// 设置或获取错误代码
        /// </summary>
        public int code { set; get; }

        /// <summary>
        /// 设置或获取错误信息
        /// </summary>
        public string msg { set; get; }

        /// <summary>
        /// 设置或获取详细信息
        /// </summary>
        public string detail { set; get; }
    }
}
