using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace Common
{
    public static class Function
    {
        #region 改动后的MD5
        /// <summary>
        /// 改动后的MD5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMd5(string str)
        {
            string cl = "&8e(t)ed" + str;
            //string cl = "&8eed" + DateTime.Now.Month + str + DateTime.Now.Day;//将真实验证码加上前缀与后缀后再加密；
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            s.Reverse();    //翻转生成的MD5码
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 6; i < s.Length - 1; i++) //只取MD5码的一部分；恶意访问者无法知道我取的是哪几位。
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符
                pwd = pwd + (s[i] < 198 ? s[i] + 28 : s[i]).ToString("X"); // 进一步对生成的MD5码做一些改造。
            }
            return pwd;
        }
        #endregion

        #region 过滤非法字符
        /// <summary>
        /// SQL过滤敏感字符
        /// </summary>
        /// <param name="inText">要特殊过滤的字符串</param>
        /// <returns>过滤后的字符串</returns>
        public static string SqlFilter(string inText = "")
        {
            if (string.IsNullOrEmpty(inText)) //如果字符串为空，直接返回。
            {
                return "";
            }
            inText = inText.Replace("\" ", " ");
            inText = inText.Replace("or ", " ");
            inText = inText.Replace("* ", " ");
            inText = inText.Replace("count( ", " ");
            inText = inText.Replace("drop   table   ", " ");
            inText = inText.Replace("update   ", " ");
            inText = inText.Replace("truncate   ", " ");
            inText = inText.Replace("asc( ", " ");
            inText = inText.Replace("mid( ", " ");
            inText = inText.Replace("char( ", " ");
            inText = inText.Replace("xp_cmdshell ", " ");
            inText = inText.Replace("exec   master ", " ");
            inText = inText.Replace("net   localgroup   administrators ", " ");
            inText = inText.Replace("   and   ", " ");
            inText = inText.Replace("net   user ", " ");
            inText = inText.Replace("   or   ", " ");
            inText = inText.Replace("and ", "");
            inText = inText.Replace("exec ", "");
            inText = inText.Replace("insert ", "");
            inText = inText.Replace("select ", "");
            inText = inText.Replace("delete ", "");
            inText = inText.Replace("update ", "");
            inText = inText.Replace(" and", "");
            inText = inText.Replace(" exec", "");
            inText = inText.Replace(" insert", "");
            inText = inText.Replace(" select", "");
            inText = inText.Replace(" delete", "");
            inText = inText.Replace(" update ", "");
            inText = inText.Replace("chr ", "");
            inText = inText.Replace("mid ", "");
            inText = inText.Replace(" chr", "");
            inText = inText.Replace(" mid", "");
            inText = inText.Replace("master ", "");
            inText = inText.Replace(" master", "");
            inText = inText.Replace("or ", "");
            inText = inText.Replace(" or", "");
            inText = inText.Replace("truncate ", "");
            inText = inText.Replace("char ", "");
            inText = inText.Replace("declare ", "");
            inText = inText.Replace("join ", "");
            inText = inText.Replace("union ", "");
            inText = inText.Replace("truncate ", "");
            inText = inText.Replace(" char", "");
            inText = inText.Replace(" declare", "");
            inText = inText.Replace(" join", "");
            inText = inText.Replace(" union", "");
            inText = inText.Replace("'", "");
            inText = inText.Replace("<", "");
            inText = inText.Replace(">", "");
            inText = inText.Replace("%", "");
            inText = inText.Replace("'delete", "");
            inText = inText.Replace("''", "");
            inText = inText.Replace("\"\"", "");
            inText = inText.Replace(",", "");
            inText = inText.Replace(">=", "");
            inText = inText.Replace("=<", "");
            inText = inText.Replace("--", "");
            inText = inText.Replace(";", "");
            inText = inText.Replace("||", "");
            inText = inText.Replace("[", "");
            inText = inText.Replace("]", "");
            inText = inText.Replace("&", "");
            inText = inText.Replace("/", "");
            inText = inText.Replace("?", "");
            inText = inText.Replace(">?", "");
            inText = inText.Replace("?<", "");
            inText = inText.Replace(" ", "");
            return inText;
        }
        #endregion

        /// <summary>
        /// 加密数据
        /// </summary>
        /// <param name="text"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string Encrypt(string text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(text);
            MD5 md5Hash = MD5.Create();
            des.Key = Encoding.ASCII.GetBytes(md5Hash.ComputeHash(Encoding.UTF8.GetBytes(sKey)).ToString().Substring(0, 8));
            des.IV = Encoding.ASCII.GetBytes(md5Hash.ComputeHash(Encoding.UTF8.GetBytes(sKey)).ToString().Substring(0, 8));
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        /// <summary>
        /// 解密数据
        /// </summary>
        /// <param name="text"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string Decrypt(string text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            MD5 md5Hash = MD5.Create();
            des.Key = Encoding.ASCII.GetBytes(md5Hash.ComputeHash(Encoding.UTF8.GetBytes(sKey)).ToString().Substring(0, 8));
            des.IV = Encoding.ASCII.GetBytes(md5Hash.ComputeHash(Encoding.UTF8.GetBytes(sKey)).ToString().Substring(0, 8));
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

        /// <summary>
        /// sql过滤
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static T EntityFilter<T>(T entity)
        {
            foreach (var property in entity.GetType().GetProperties())
            {
                if (property.PropertyType == typeof(string))
                {
                    var value = property.GetValue(entity)?.ToString().Trim();
                    if (!string.IsNullOrEmpty(value))
                        property.SetValue(entity, SqlFilter(value));
                }
            }
            return entity;
        }

        public static void ExceptionThrow(string errorName, string errormsg)
        {

            throw new Exception("{" + $"\"ErrorName\":\"{errorName}\",\"ErrorMsg\":\"{errormsg}\"" + "}");
        }
    }
}
