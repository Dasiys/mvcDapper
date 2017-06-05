using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.UnitOfWork;
using Dapper;
using IDAL;
using Microsoft.Win32;
using Model.Dtos;
using Model.MetadataModel;

namespace DAL
{
    public class NewsDal:BaseDal<TB_News>,INewsDal
    {
        public NewsDal(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private const string TableName = "TB_News";

        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <returns></returns>
        public IList<NewsListModel> GetNewsList(CateType cateId, int recommend=1)
        {
            var filed = "top 10 NewsId,Title,Img,InputTime,Summary";
            var condition = "Recommend=@Recommend and CateId=@CateId";
            var param = new DynamicParameters();
            param.Add("Recommend", recommend);
            param.Add("CateId", cateId);
            return GetModels<NewsListModel>(TableName, condition, param, filed);
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
            return  GetSingleModel<NewsDetailModel>(TableName, condition, param, field);
        }
    }
}
