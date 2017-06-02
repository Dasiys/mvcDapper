using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dapper;
using Model.MetadataModel;
using IBLL;
using IDAL;
using Common;
using Model.Dtos;
using System.Web;
using System.Web.Security;
using Model;
using System.Configuration;
using System.Net.WebSockets;
using Common.NLog;
using Newtonsoft.Json;

namespace BLL
{
    public class UserService : BaseService<TB_UserInfo>, IUserService
    {
        private readonly IUserDal _userDal;
        private const string TableName = "TB_UserInfo";

        public UserService(IUserDal userDal, ILogFactory logFactory) : base(userDal, logFactory)
        {
            _userDal = userDal;
        }


        /// <summary>
        /// 设置或获取用户基础信息
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public TB_UserInfo GetUserAccountInfo(string mobile, string password)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("Mobile", Function.SqlFilter(mobile.Trim()));
            param.Add("Password", Function.GetMd5(Function.SqlFilter(password.Trim())));
            var condition = "Mobile=@Mobile and password=@Password";
            var fields = "Uid as Id,Mobile,Contact,IsShop";
            var result = _userDal.GetSingleModel<TB_UserInfo>(TableName, condition, param, fields);
            if (result == null)
                ExceptionThrow("LoginError", "请输入正确的用户名和密码");
            return result;
        }

        /// <summary>
        /// 获取图形验证码
        /// </summary>
        /// <param name="vertifyCode"></param>
        /// <returns></returns>
        public byte[] GetVertifyCode(out string vertifyCode)
        {
            var tool = new VerifyCodeTools();
            vertifyCode = tool.CreateVertifyCode();
            var result = tool.CreateValidateGraphic(vertifyCode);
            vertifyCode = Function.GetMd5(vertifyCode);
            return result;
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="model"></param>
        public void RegisterUser(UserRegisterModel model)
        {
            Check(model);
            DynamicParameters param = new DynamicParameters();
            param.Add("Mobile", model.Mobile.Trim());
            var count = _userDal.GetSingleModel<int>(TableName, "Mobile=@Mobile", param, "count(1)");
            if (count > 0)
                ExceptionThrow("Existed", "此号码已存在");
            param.Add("CompanyName", model.CompanyName);
            param.Add("Password", Function.GetMd5(model.Password));
            param.Add("Contact", model.Contact);
            param.Add("RegType", model.RegType);
            var result = _userDal.ExcuteProc("Proc_RegAdd", param);
            if (result < 1)
            {
                _LogFactory.Error($"{ServiceName}:{new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name}", "注册失败", model);
                ExceptionThrow("Error", "注册失败");
            }
            model.Id = result;
        }

        /// <summary>
        /// 检查注册数据
        /// </summary>
        /// <param name="model"></param>
        private void Check(UserRegisterModel model)
        {
            Function.EntityFilter<UserRegisterModel>(model);
            if (!IsMobile(model.Mobile))
                ExceptionThrow("MobileError", "请输入正确格式的手机号码");

            if (!IsPass(model.Password))
                ExceptionThrow("PassError", "请输入正确格式的密码");

            var vertifyCode = Context.Request.Cookies["cqmyg365"]?.Value;
            if (string.IsNullOrEmpty(vertifyCode))
                ExceptionThrow("VertifyCodeError", "验证码失效");
            if (Function.GetMd5(model.VertifyCode) != vertifyCode)
                ExceptionThrow("VertifyCodeError", "请输入正确的验证码");

            //model.MobileMessage = Function.SqlFilter(model.MobileMessage?.Trim());
            //if (string.IsNullOrEmpty(model.CookieMobileMessage))
            //    ExceptionThrow("MobileMessageFailed", "手机验证码失效");
            //if (Function.GetMd5(model.MobileMessage) != model.CookieMobileMessage)
            //    ExceptionThrow("MobileMessageError", "请输入正确的手机验证码");
        }

        /// <summary>
        /// 登录后保存用户信息
        /// </summary>
        /// <param name="memberInfo"></param>
        public void SaveUserInfo(CMemberInfo memberInfo)
        {
            var responseCookies = Context.Response.Cookies;
            var token = Guid.NewGuid().ToString();
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, memberInfo.MemberId.ToString(), DateTime.Now, DateTime.Now.AddMinutes(20), false, token);
            if (Context.Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                responseCookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddDays(-1);
            }
            responseCookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket)));
            Context.Session[token] = memberInfo;
        }

        /// <summary>
        /// 判断是否是正确格式的手机号
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        private bool IsMobile(string mobile)
        {
            Regex r = new Regex(@"^(13|15|18|17)\d{9}$");
            return r.IsMatch(mobile);
        }

        /// <summary>
        /// 验证密码
        /// </summary>
        /// <returns></returns>
        private bool IsPass(string password)
        {
            Regex r = new Regex(@"^[a-zA-Z]{1}([a-zA-Z0-9]){5,}$");
            return r.IsMatch(password);
        }

        /// <summary>
        /// 发送手机验证码
        /// </summary>
        /// <param name="vertifyCode"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public async Task SendMobileMsg(string vertifyCode, string mobile)
        {
            if (Context.Request.Cookies["cqmyg365"] != null)
            {
                string chkcode = Context.Request.Cookies["cqmyg365"].Value; //获得系统生成的验证码

                string mob = Function.SqlFilter(mobile.Trim());
                string strYz = Function.SqlFilter(vertifyCode.Trim());

                if (chkcode == Function.GetMd5(strYz))
                {
                    string str = "";
                    Random rd = new Random(unchecked((int)(DateTime.Now.Ticks >> 1)));
                    for (int j = 0; j < 6; j++)
                    {
                        int num = rd.Next();
                        str = str + ((char)(0x30 + ((ushort)(num % 10)))).ToString();
                    }

                    HttpCookie cokMsg = new HttpCookie("ysdhs1024", Function.GetMd5(str));
                    cokMsg.Expires = DateTime.Now.AddMinutes(3);
                    Context.Response.Cookies.Add(cokMsg); // 使用Cookies取验证码的值
                    var dic = new Dictionary<string, string>
                    {
                        {"apikey", "6be95a763992bea57f841647a9618f27"},
                        {"text", Uri.EscapeDataString($"【搜木网】您的验证码是{str}")},
                        {"mobile", mob}
                    };
                    var result = JsonConvert.DeserializeObject<YunPianErrorModel>(await HttpPostAsync(dic));
                    if (result.code != 0)
                    {
                        _LogFactory.Error("UserService:SendMobileMsg", result.detail, result);
                        ExceptionThrow("MobileMessage", "发送验证码失败，请稍后再试");
                    }
                }
                else
                {
                    ExceptionThrow("VertifyCodeError", "验证码输入错误");
                }
            }
            else
            {
                ExceptionThrow("VertifyCodeError", "验证码失效");
            }
        }

        /// <summary>
        /// 请求接口
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private async Task<string> HttpPostAsync(Dictionary<string, string> parameters)
        {
            var url = ConfigurationManager.AppSettings["BaseUri"];
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.None };

            var httpClient = new HttpClient(handler) { BaseAddress = new Uri(url) };

            httpClient.SendAsync(new HttpRequestMessage
            {
                Method = new HttpMethod("HEAD"),
                RequestUri = new Uri(url + "/")
            }).Result.EnsureSuccessStatusCode();
            var response = await httpClient.PostAsync(url, new FormUrlEncodedContent(parameters));
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// 登出
        /// </summary>
        public void Logout()
        {
            if (Context.Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                Context.Response.Cookies[FormsAuthentication.FormsCookieName].Expires=DateTime.Now.AddDays(-1);
            Context.Session.RemoveAll();
        }

        /// <summary>
        /// 设置登录信息（保存到cookie里面或者从cookie里面删除）
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="password"></param>
        /// <param name="autoLogin"></param>
        public void OperateLoginInfo(string mobile, string password, bool autoLogin)
        {
            var requestCookies = Context.Request.Cookies;
            var responseCookies = Context.Response.Cookies;
            if (autoLogin && requestCookies["LoginInfo"] == null)
            {
                var loginIinfo = new LoginInfo()
                {
                    Mobile = Function.Encrypt(mobile, "phoneNum"),
                    Password = Function.Encrypt(password, "passKey")
                };
                responseCookies.Add(new HttpCookie("LoginInfo", JsonConvert.SerializeObject(loginIinfo)) { Expires = DateTime.Now.AddDays(30) });
            }
            if (!autoLogin && requestCookies["LoginInfo"] != null)
            {
                responseCookies["LoginInfo"].Expires = DateTime.Now.AddDays(-1);
            }
        }

        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <returns></returns>
        public LoginInfo GetLoginInfo()
        {
            var info = Context.Request.Cookies["LoginInfo"]?.Value;
            if (info!=null)
            {
                var loginInfo = JsonConvert.DeserializeObject<LoginInfo>(info);
                return new LoginInfo()
                {
                    Mobile = Function.Decrypt(loginInfo.Mobile, "phoneNum"),
                    Password = Function.Decrypt(loginInfo.Password, "passKey")
                };
            }
            return null;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="newPassword"></param>
        /// <param name="oldPassword"></param>
        /// <param name="id"></param>
        public void UpdatePassword(string newPassword, string oldPassword, int id)
        {
            var param = new DynamicParameters();
            param.Add("Uid", id);
            param.Add("Password", Function.GetMd5(Function.SqlFilter(oldPassword.Trim())));
            var condition = " Uid=@Uid and Password=@Password";
            var result = GetSingleModel<int>(TableName, condition, param, "count(1)");
            if (result < 1)
                ExceptionThrow("Error", "密码输入错误");

            newPassword = Function.SqlFilter(newPassword.Trim());
            if (!IsPass(newPassword))
                ExceptionThrow("ErrorNewPas", "密码格式错误");

            var uParam = new DynamicParameters();
            uParam.Add("Password", Function.GetMd5(newPassword));
            uParam.Add("Uid", id);
            var sql = $"update {TableName} set Password=@Password where Uid=@Uid";
            result = Excute(sql, uParam);
            if (result < 1)
            {
                _LogFactory.Error($"{ServiceName}:{new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name}","修改密码失败",new {Uid=id,Password=newPassword});
                ExceptionThrow("Error", "修改失败，请重试");
            }
            OperateLoginInfo("", "", false);
        }

        /// <summary>
        /// 获取个人信息详情
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public UserInfoModifyModel GetUserInfoDetail(int uid)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("Uid", uid);
            var condition = "Uid=@uid";
            var field = "CompanyName,Mobile,Contact,Email,QQ,Tel,Addr,Photo,Uid";
            var result = _userDal.GetSingleModel<UserInfoModifyModel>(TableName, condition, param, field);
            if (result == null)
            {
                _LogFactory.Error($"{ServiceName}:{new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name}","获取用户信息失败",new {Uid=uid});
                ExceptionThrow("Error", "获取用户信息失败，请稍后再试");
            }
            return result;
        }

        /// <summary>
        /// 更改用户信息
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateUserInfo(UserInfoModifyModel entity)
        {
            var sql =
                $"Update {TableName} Set CompanyName=@CompanyName,Mobile=@Mobile,Contact=@Contact,Email=@Email,QQ=@QQ,Tel=@Tel,Addr=@Addr,Photo=@Photo " +
                "where Uid=@Uid";
            var result = _userDal.Excute(sql, entity);
            if (result < 1)
            {
                _LogFactory.Error($"{ServiceName}:{new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name}","修改用户信息失败",entity);
                ExceptionThrow("Error", "修改失败，请重试");
            }
        }

        public HttpContext Context { get; set; }
        private string ServiceName => GetType().Name;
    }
}
