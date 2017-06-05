using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Model.MetadataModel;
using System.ComponentModel.DataAnnotations;

namespace Model.ViewModel
{
    /// <summary>
    /// 用户信息更改Model
    /// </summary>
    public class UserRegisterModel
    {
        /// <summary>
        /// 设置或获取Id
        /// </summary>
        public int Id { set; get; }
        /// <summary>
        /// 设置或获取公司名称
        /// </summary>
        [Required(AllowEmptyStrings = false,ErrorMessage = "请填写公司名称")]
        public string CompanyName { set; get; }

        /// <summary>
        /// 设置或获取电话号码
        /// </summary>
        [Required(AllowEmptyStrings = false,ErrorMessage = "请填写手机号码")]
        [StringLength(11,ErrorMessage = "手机号码长度不可以超过16位")]
        public string Mobile { set; get; }

        /// <summary>
        /// 设置或获取密码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请填写密码")]
        public string Password { set; get; }

        /// <summary>
        /// 设置或获取用户名
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请填写姓名")]
        public string Contact { set; get; }

        /// <summary>
        /// 设置或获取注册类型，默认为App注册
        /// </summary>
        public RegType RegType { set; get; } = RegType.APP;

        /// <summary>
        /// 设置或获取手机验证码
        /// </summary>
        //[Required(AllowEmptyStrings = false, ErrorMessage = "请填写手机验证码")]
        public string MobileMessage { set; get; }

        /// <summary>
        /// 设置或获取验证码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请填写验证码")]
        public string VertifyCode { set; get; }
    }
}
