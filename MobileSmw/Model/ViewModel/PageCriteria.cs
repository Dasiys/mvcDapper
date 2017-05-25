using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    /// <summary>
    /// 页面标准
    /// </summary>
    public class PageCriteria
    {
        /// <summary>
        /// 设置或获取表名
        /// </summary>
        public string TableName { set; get; }

        /// <summary>
        /// 设置或获取查询字段
        /// </summary>
        public string Fields { set; get; }

        /// <summary>
        /// 设置或获取主键
        /// </summary>
        public string PrimaryKey { set; get; }

        /// <summary>
        /// 设置或获取每页显示数目
        /// </summary>
        public int PageSize { set; get; }

        /// <summary>
        /// 设置或获取当前页面
        /// </summary>
        public int CurrentPage { set; get; }

        /// <summary>
        /// 设置或获取排序
        /// </summary>
        public string Sort { set; get; }

        /// <summary>
        /// 设置或获取查询条件
        /// </summary>
        public string Condition { set; get; }

        /// <summary>
        /// 设置或获取总数目
        /// </summary>
        public int RecordCount { set; get; }
    }
}
