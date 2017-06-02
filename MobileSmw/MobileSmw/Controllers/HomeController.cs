using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using IBLL;
using Model;

namespace MobileSmw.Controllers
{
    public class HomeController : ErrorController
    {
        private readonly IOrderService _orderService;

        public HomeController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        private CMemberInfo GetCmemberInfo()
        {
            var cookie = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            var token = string.IsNullOrEmpty(cookie?.Value) ? "" : FormsAuthentication.Decrypt(cookie.Value)?.UserData;
            if (!string.IsNullOrEmpty(token))
            {
                var seesionObj = Session[token];
                if (seesionObj != null)
                {
                    return (CMemberInfo)seesionObj;
                }
            }
            return null;
        }
        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var memnerInfo = GetCmemberInfo();
                ViewBag.OrderQuantity = _orderService.GetOrderQuantity(memnerInfo.MemberId);
                return View();

            }
            return new EmptyResult();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}