﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOC;
using Model.MetadataModel;
using Model.ViewModel;

namespace IDAL
{
    public interface INewsDal:IBaseDal<TB_News>, IDependency
    {
        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <returns></returns>
        IList<NewsListModel> GetNewsList(CateType cateId, int recommend = 1);

        /// <summary>
        /// 获取新闻详情
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        NewsDetailModel GetNewsDetail(int newsId);
    }
}
