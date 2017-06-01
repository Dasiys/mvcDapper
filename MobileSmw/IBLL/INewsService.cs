using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOC;
using Model.Dtos;
using Model.MetadataModel;

namespace IBLL
{
    public interface INewsService:IBaseService<TB_News>, IDependency
    {
        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <returns></returns>
        IList<NewsListModel> GetNewsList(CateType cateId);

        /// <summary>
        /// 获取新闻详情
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        NewsDetailModel GetNewsDetail(int newsId);
    }
}
