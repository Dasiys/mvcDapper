using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml.Linq;
using Common;
using Model;
using Model.MetadataModel;
using IBLL;
using Model.Dtos;

namespace MobileSmw.Controllers
{
    public class AccountController : ErrorController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
            _userService.Context = System.Web.HttpContext.Current;
        }
        public ActionResult Login(string ReturnUrl = "")
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="password"></param>
        /// <param name="returnUrl"></param>
        /// <param name="autoLogin">1为自动登录</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(string mobile, string password, string returnUrl,int autoLogin)
        {
            var userInfo = _userService.GetUserInfo(mobile, password);
            if (userInfo == null)
            {
                ModelState.AddModelError("LoginError", "请输入正确的用户名和密码");
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }
            _userService.OperateLoginInfo(mobile,password,autoLogin==1);
            _userService.SaveUserInfo(new CMemberInfo()
            {
                MemberId = userInfo.Id,
                LoginId = userInfo.Mobile,
                Name = userInfo.Contact,
                MemberType = userInfo.IsShop ? 1 : 0
            });
            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 返回登录信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetLoginInfo()
        {
            return ReturnResultHelper.ReturnResult( new ResultModel()
            {
                Data = _userService.GetLoginInfo()
            });
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(UserRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                
                _userService.RegisterUser(model);
               _userService.SaveUserInfo(new CMemberInfo()
                {
                    MemberId = model.Id,
                    LoginId = model.Mobile,
                    Name = model.Contact,
                    MemberType = 0
                });
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        /// <summary>
        /// 获取图片验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult GetValidatorGraphics()
        {
            var vertifycode = "";
            var result=_userService.GetVertifyCode(out vertifycode);
            if(Request.Cookies["cqmyg365"]!=null)
                Request.Cookies.Remove("cqmyg365");
            Response.Cookies.Add(new HttpCookie("cqmyg365",vertifycode){Expires = DateTime.Now.AddMinutes(2)});
            return File(result,@"image/jpeg");
        }

        /// <summary>
        /// 发送手机验证码
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="vertifyCode"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> SendMobileMsg(string mobile, string vertifyCode)
        {
            await _userService.SendMobileMsg(vertifyCode, mobile);
            return ReturnResultHelper.ReturnResult(new ResultModel());
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            _userService.Logout();
            return RedirectToAction("Login", "Account");
        }


    }
}