using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ErrorModel
    {
        /// <summary>
        /// 设置或获取错误名称
        /// </summary>
        public string ErrorName { set; get; }

        /// <summary>
        /// 设置或获取错误信息
        /// </summary>
        public string ErrorMsg { set; get; }
    }
}
