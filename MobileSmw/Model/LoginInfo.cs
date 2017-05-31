using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 登录信息
    /// </summary>
    public class LoginInfo
    {
        /// <summary>
        /// 设置或获取手机号码
        /// </summary>
        public string Mobile { set; get; }

        /// <summary>
        /// 设置或获取密码
        /// </summary>
        public string Password { set; get; }
    }
}
