using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBLL;
using Model.ViewModel;

namespace MobileSmw.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
            _userService.Context=System.Web.HttpContext.Current;
        }

        public ActionResult UpdatePassword()
        {
            return View();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdatePassword(string oldPassword,string newPassword)
        {
            _userService.UpdatePassword(newPassword,oldPassword,CMemberInfo.MemberId);
            return RedirectToAction("Index","Home");
        }

        public ActionResult UpdateUserInfo()
        {
            return View(_userService.GetUserInfoDetail(CMemberInfo.MemberId));
        }

        /// <summary>
        /// 更改用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateUserInfo(UserInfoModifyModel model)
        {
            model.Uid = CMemberInfo.MemberId;
            _userService.UpdateUserInfo(model);
            return View(model);
        }
    }
}