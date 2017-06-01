using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private const string TableName = "TB_News";

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
            var filed = "top 10 NewsId,Title,Img,InputTime,Summary";
            var condition = "Recommend=@Recommend and CateId=@CateId";
            var param = new DynamicParameters();
            param.Add("Recommend", 1);
            param.Add("CateId", cateId);
            return _newsDal.GetModels<NewsListModel>(TableName, condition, param, filed);
        }
        /// <summary>
        /// 获取新闻详情
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public NewsDetailModel GetNewsDetail(int newsId)
        {
            var field = "NewsId,Title,Img,InputTime,Content";
            var condition = "NewsId=@NewsId";
            var param = new DynamicParameters();
            param.Add("NewsId", newsId);
            var result = _newsDal.GetSingleModel<NewsDetailModel>(TableName, condition, param, field);
            if(result==null)
                ExceptionThrow("Error","获取动态走势失败，请稍后再试");
            return result;
        }
    }
}
