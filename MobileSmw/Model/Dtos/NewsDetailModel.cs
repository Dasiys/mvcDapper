using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dtos
{
    /// <summary>
    /// 行业资讯详情
    /// </summary>
    public class NewsDetailModel
    {
        /// <summary>
        /// 设置或获取新闻Id
        /// </summary>
        public int NewsId { set; get; }
        /// <summary>
        /// 设置或获取标题
        /// </summary>
        public string Title { set; get; }
        /// <summary>
        /// 设置或获取文章内容
        /// </summary>
        public string Content { set; get; }
        /// <summary>
        /// 设置或获取上传时间
        /// </summary>
        public DateTime InputTime { set; get; }
    }
}
