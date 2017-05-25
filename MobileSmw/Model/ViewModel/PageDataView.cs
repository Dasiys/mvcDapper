using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.MetadataModel;

namespace Model.ViewModel
{
    public class PageDataView<T> where T:class , IEntityBase,new()
    {
        /// <summary>
        /// 设置或获取总数目
        /// </summary>
        public int RecordCount { set; get; }

        /// <summary>
        /// 设置或获取总记录
        /// </summary>
        public List<T> Items { set; get; }

        /// <summary>
        /// 设置或获取当前所在页数
        /// </summary>
        public int CurrentPage { set; get; }

        /// <summary>
        /// 设置或获取总页数
        /// </summary>
        public int TotalPageCount { set; get; }
    }
}
