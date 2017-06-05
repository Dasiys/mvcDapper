using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    /// <summary>
    /// 新闻列表模型
    /// </summary>
    public class NewsListModel
    {
        /// <summary>
        /// 设置或获取新闻Id
        /// </summary>
        public int NewsId { set; get; }
        /// <summary>
        /// 设置或获取标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 设置或获取图片
        /// </summary>
        public string Img { get; set; }
        /// <summary>
        /// 设置或获取上传时间
        /// </summary>
        public System.DateTime InputTime { get; set; }
        /// <summary>
        /// 设置或获取文章总结
        /// </summary>
        public string Summary { get; set; }
    }
}
