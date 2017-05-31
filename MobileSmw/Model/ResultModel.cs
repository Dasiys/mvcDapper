using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 返回结果
    /// </summary>
    public class ResultModel
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg { set; get; } = "";

        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { set; get; } = null;

        /// <summary>
        /// 返回多条数据
        /// </summary>
        public ICollection<object> DataCollection { set; get; } = null;

        /// <summary>
        /// 调用接口是否成功
        /// </summary>
        public bool IsSuccess { set; get; } = true;
    }
}
