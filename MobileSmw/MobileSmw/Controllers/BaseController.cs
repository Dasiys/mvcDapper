using System.Web.Mvc;
using System.Web.Security;
using Model;

namespace MobileSmw.Controllers
{
    /// <summary>
    /// 身份验证
    /// </summary>
    [Authorize]
    public class BaseController : ErrorController
    {
        protected CMemberInfo CMemberInfo = new CMemberInfo();
        /// <summary>
        /// 重写调用方法前的调用
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var request = filterContext.HttpContext.Request;
            var actionDescriptor = filterContext.ActionDescriptor;
            string returnUrl = $"/{actionDescriptor.ControllerDescriptor.ControllerName}/{actionDescriptor.ActionName}";
            var cookie = request.Cookies[FormsAuthentication.FormsCookieName];
            var token = string.IsNullOrEmpty(cookie?.Value)?"":FormsAuthentication.Decrypt(cookie.Value)?.UserData;
            if (string.IsNullOrEmpty(token))
            {
                filterContext.Result = LoginResult(returnUrl);
                return;
            }
            var session =filterContext.HttpContext.Session[token];
            if (session == null)
            {
                filterContext.Result = LoginResult(returnUrl);
                return;
            }
            CMemberInfo = (CMemberInfo) session;
        }

        public virtual ActionResult LoginResult(string ReturnUrl)
        {
            return RedirectToAction("Login", "Account", new { ReturnUrl});
        }
    }
}