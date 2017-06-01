using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BLL
{
    public static class XiMuClient
    {
        /// <summary>
        /// 带有ssl证书的post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="Params"></param>
        /// <param name="pathClientP12"></param>
        /// <param name="certFilePwd"></param>
        /// <returns></returns>
        public static string HttpPostSsl(string url, string Params, string pathClientP12, string certFilePwd)
        {
            String sResult = "";
            if (url == "" || Params == "")
            {
                return sResult;
            }
            try
            {
                Encoding encoding = Encoding.GetEncoding("utf-8");
                byte[] data = encoding.GetBytes(Params);
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

                X509Certificate2 clientP12 = new X509Certificate2(AppDomain.CurrentDomain.BaseDirectory + pathClientP12, certFilePwd);
                webRequest.ClientCertificates.Add(clientP12);
                if (ServicePointManager.ServerCertificateValidationCallback == null)
                    ServicePointManager.ServerCertificateValidationCallback = ((sender1, certificate, chain, sslPolicyErrors) => true);

                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = data.Length;
                webRequest.Method = "POST";
                Stream webStream = webRequest.GetRequestStream();

                webStream.Write(data, 0, data.Length);
                webStream.Close();

                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                StreamReader reader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);
                sResult = reader.ReadToEnd();

                sResult.Trim();
            }
            catch (Exception e)
            {
                sResult = e.ToString();
                return sResult;
            }

            return sResult;
        }
    }
}
