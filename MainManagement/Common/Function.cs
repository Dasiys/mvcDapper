using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace Common
{
    public static class Function
    {
        /// <summary>
        /// 字符串转换为GUID
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Guid stringToGuid(string str)
        {
            try
            {
                if (str == "")
                {
                    return Guid.Empty;
                }
                GuidConverter xConverter = new GuidConverter();
                if (xConverter.IsValid(str))
                {
                    return new Guid(str);
                }
                else
                {
                    throw new ArgumentException("参数错误：输入的 strGuid 非 GUID格式。");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
