using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.MetadataModel;
using IOC;
using Model.Dtos;
using System.Web;
using Model;

namespace IBLL
{
    public interface IUserService:IBaseService<TB_UserInfo>,IDependency
    {
        /// <summary>
        /// 获取用户登录信息
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        TB_UserInfo GetUserInfo(string mobile, string password);

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="vertifyCode"></param>
        /// <returns></returns>
        byte[] GetVertifyCode(out string vertifyCode);

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="model"></param>
        /// <param name="context"></param>
        void RegisterUser(UserInfoModifyModel model);

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="memberInfo"></param>
        void SaveUserInfo(CMemberInfo memberInfo);

        void OperateLoginInfo(string mobile, string password, bool autoLogin);

        /// <summary>
        /// 发送并获取手机验证码
        /// </summary>
        /// <param name="context"></param>
        /// <param name="vertifyCode"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        Task SendMobileMsg(string vertifyCode, string mobile);

        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="context"></param>
        void Logout();

        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        LoginInfo GetLoginInfo();

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="newPassword"></param>
        /// <param name="oldPassword"></param>
        /// <param name="id"></param>
        void UpdatePassword(string newPassword,string oldPassword, int id);

        HttpContext Context { set; get; }
    }
}
