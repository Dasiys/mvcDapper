using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBLL;
using Model.Dtos;

namespace MobileSmw.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }
        /// <summary>
        /// 获得动态走势列表
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        public ActionResult List(CateType cateId)
        {
            return View(_newsService.GetNewsList(cateId));
        }
        /// <summary>
        /// 获得动态走势详情
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public ActionResult GetNewsDetail(int newsId)
        {
            return View(_newsService.GetNewsDetail(newsId));
        }
    }
}