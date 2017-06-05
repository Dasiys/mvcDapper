using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class UserInfoModifyModel
    {
        /// <summary>
        /// 设置或获取用户Id
        /// </summary>
        public int Uid { set; get; }
        /// <summary>
        /// 设置或获取公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 设置或获取手机号码
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 设置或获取姓名
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 设置或获取邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 设置或获取QQ
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// 设置或获取固话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 设置或获取地址
        /// </summary>
        public string Addr { get; set; }
        /// <summary>
        /// 设置或获取头像
        /// </summary>
        public string Photo { get; set; }
    }
}
