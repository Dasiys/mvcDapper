using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using Newtonsoft.Json;

namespace MobileSmw.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// 拦截错误信息
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                var error = JsonConvert.DeserializeObject<ErrorModel>(filterContext.Exception.Message);
                if (View().View != null)
                {
                    ModelState.AddModelError(error.ErrorName, error.ErrorMsg);
                    filterContext.Result = View();
                }
                else
                {
                    Response.Write(JsonConvert.SerializeObject(new ResultModel()
                    {
                        ErrorMsg = error.ErrorMsg,
                        IsSuccess = false
                    }));
                }
                filterContext.ExceptionHandled = true;
            }
            base.OnActionExecuted(filterContext);
        }
    }
}