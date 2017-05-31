using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 缓存存储的用户信息
    /// </summary>
    public class CMemberInfo
    {
        /// <summary>
        /// 设置或获取会员Id
        /// </summary>
        public int MemberId { set; get; }

        /// <summary>
        /// 设置或获取登录Id
        /// </summary>
        public string LoginId { set; get; }

        /// <summary>
        /// 设置获取用户姓名
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 设置或获取会员类型
        /// </summary>
        public int MemberType { set; get; }
    }
}
