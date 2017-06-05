using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.NLog;
using Dapper;
using IBLL;
using IDAL;
using Model.Dtos;
using Model.MetadataModel;

namespace BLL
{
    /// <summary>
    /// 行业信息
    /// </summary>
    public class NewsService : BaseService<TB_News>, INewsService
    {
        private readonly INewsDal _newsDal;

        public NewsService(INewsDal newsDal, ILogFactory logFactory) : base(newsDal, logFactory)
        {
            _newsDal = newsDal;
        }

        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <returns></returns>
        public IList<NewsListModel> GetNewsList(CateType cateId)
        {
            return _newsDal.GetNewsList(cateId);
        }
        /// <summary>
        /// 获取新闻详情
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public NewsDetailModel GetNewsDetail(int newsId)
        {
            var result = _newsDal.GetNewsDetail(newsId);
            if (result == null)
            {
                _LogFactory.Error($"{GetType().Name}:{new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name}","获取动态走势失败",new {NewsId=newsId});
                Function.ExceptionThrow("Error", "获取动态走势失败，请稍后再试");
            }
            return result;
        }
    }
}
