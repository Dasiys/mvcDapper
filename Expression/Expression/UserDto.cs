using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expression
{
    public class UserDto
    {
        /// <summary>
        /// 设置或获取名称
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 设置或获取尊称
        /// </summary>
        public string RespectName => $"尊称:{Name}";
    }
}
