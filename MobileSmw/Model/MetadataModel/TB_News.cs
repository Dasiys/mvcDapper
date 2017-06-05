using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ViewModel;

namespace Model.MetadataModel
{
    /// <summary>
    /// 行业资讯
    /// </summary>
    public class TB_News:IEntityBase
    {
        /// <summary>
        /// 设置或获取新闻Id
        /// </summary>
        [Column(Name= "NewsID")]
        public int Id { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public CateType CateID { get; set; }
        public string Title { get; set; }
        public string sTitle { get; set; }
        public string Tag { get; set; }
        public string Summary { get; set; }
        public string Img { get; set; }
        public string Content { get; set; }
        /// <summary>
        /// 设置或获取是否推荐
        /// </summary>
        public bool Recommend { get; set; }
        /// <summary>
        /// 设置或获取是否检查
        /// </summary>
        public bool IsCheck { get; set; }
        public System.DateTime InputTime { get; set; }
    }
}
