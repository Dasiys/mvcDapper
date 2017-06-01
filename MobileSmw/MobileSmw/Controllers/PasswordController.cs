using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBLL;

namespace MobileSmw.Controllers
{
    public class PasswordController : BaseController
    {
        private readonly IUserService _userService;

        public PasswordController(IUserService userService)
        {
            _userService = userService;
            _userService.Context=System.Web.HttpContext.Current;
        }

        public ActionResult Update()
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
        public ActionResult Update(string oldPassword,string newPassword)
        {
            _userService.UpdatePassword(newPassword,oldPassword,CMemberInfo.MemberId);
            return RedirectToAction("Index","Home");
        }
    }
}