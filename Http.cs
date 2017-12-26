using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;

namespace bailian
{
    class Http
    {
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }

        public static string Timestamp()
        {
            TimeSpan span = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            return ((ulong)span.TotalMilliseconds).ToString();
        }

        public static void login()
        {
            HttpWebRequest request = null;
            CookieContainer myCookieContainer = new CookieContainer();

            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            request = WebRequest.Create(@"https://hwid1.vmall.com/CAS/portal/login.html") as HttpWebRequest;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "GET";
            request.Accept = "text/html, application/xhtml+xml, image/jxr, */*";
            request.Host = "hwid1.vmall.com";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36 Edge/16.16299";
            WebHeaderCollection headers = request.Headers;
            headers.Add("Accept-Language","zh-Hans-CN,zh-Hans;q=0.5");
            headers.Add("Accept-Encoding", "gzip, deflate, br");
            request.CookieContainer = myCookieContainer;
            WebResponse response = request.GetResponse();
            string cookieString = response.Headers["Set-Cookie"];
            Console.WriteLine(string.Format("2:{0}", cookieString));


            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            request = WebRequest.Create(@"https://hwid1.vmall.com/CAS/ajaxHandler/riskLogin?reflushCode=0.046584260364757046") as HttpWebRequest;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "POST";
            request.Referer = "https://hwid1.vmall.com/CAS/portal/login.html";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36 Edge/16.16299";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            request.Accept = "application/json, text/javascript, */*; q=0.01";
            request.Host = "hwid1.vmall.com";
            headers = request.Headers;
            headers.Add("Origin", "https://hwid1.vmall.com");
            headers.Add("Accept-Language", "zh-Hans-CN,zh-Hans;q=0.5");
            headers.Add("X-Requested-With", "XMLHttpRequest");
            headers.Add("Accept-Encoding", "gzip, deflate, br");
            myCookieContainer.Add(new Cookie("sid", "1049378537e9dd125c8fd9408b9e142c7ca3ef5bf151fdaca88d30abd9e8ec143e8b682642e9380537fe") { Domain = "hwid1.vmall.com", Path = "/CAS" });
            request.CookieContainer = myCookieContainer;
            StringBuilder buffer = new StringBuilder();
            buffer.AppendFormat("{0}={1}", "loginUrl", Uri.EscapeDataString("https://hwid1.vmall.com/CAS/portal/login.html"));
            buffer.AppendFormat("&{0}={1}", "service", Uri.EscapeDataString("http://www.vmall.com/"));
            buffer.AppendFormat("&{0}={1}", "loginChannel", "26000000");
            buffer.AppendFormat("&{0}={1}", "reqClientType", "26");
            buffer.AppendFormat("&{0}={1}", "lang", "zh-cn");
            buffer.AppendFormat("&{0}={1}", "userAccount", "18621076121");
            buffer.AppendFormat("&{0}={1}", "password", "Whoknows1");
            buffer.AppendFormat("&{0}={1}", "quickAuth", "false");
            buffer.AppendFormat("&{0}={1}", "isThirdBind", "0");
            buffer.AppendFormat("&{0}={1}", "remember_name", "off");
            buffer.AppendFormat("&{0}={1}", "authcode", "535031");
            buffer.AppendFormat("&{0}={1}", "opType", "8");
            buffer.AppendFormat("&{0}={1}", "authAccountType", "2");
            buffer.AppendFormat("&{0}={1}", "authAccount", "186****6121");
            Encoding requestEncoding = Encoding.GetEncoding("utf-8");
            Byte[] data = requestEncoding.GetBytes(buffer.ToString());
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }


            response = request.GetResponse();
            cookieString = response.Headers["Set-Cookie"];
            Console.WriteLine(string.Format("2:{0}", cookieString));
            
        }
       

        public static void login1()
        { 
            HttpWebRequest request=null;
            CookieContainer myCookieContainer = new CookieContainer();

            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            request = WebRequest.Create(@"https://secure.damai.cn/login.aspx?ru=https://www.damai.cn/") as HttpWebRequest;  
            request.ProtocolVersion=HttpVersion.Version10;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Safari/537.36 Edge/15.15063";
            request.CookieContainer = myCookieContainer;

            string timestamp = Timestamp();
            StringBuilder buffer = new StringBuilder();
            buffer.AppendFormat("{0}={1}", "token", timestamp);
            buffer.AppendFormat("&{0}={1}", "nationPerfix", "86");
            buffer.AppendFormat("&{0}={1}", "login_email", "18621076121");
            buffer.AppendFormat("&{0}={1}", "login_pwd", "123456");
            buffer.AppendFormat("&{0}=", "csessionid1");
            buffer.AppendFormat("&{0}=", "sig1");
            buffer.AppendFormat("&{0}=", "alitoken1");
            buffer.AppendFormat("&{0}=", "scene1");
            Encoding requestEncoding = Encoding.GetEncoding("utf-8"); 
            byte[] data = requestEncoding.GetBytes(buffer.ToString());
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }  
            
            WebResponse response = request.GetResponse();
            string cookieString = response.Headers["Set-Cookie"];
            Console.WriteLine(string.Format("2:{0}", cookieString));


            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            request = WebRequest.Create(@"https://secure.damai.cn/login.aspx?ru=https://www.damai.cn/") as HttpWebRequest;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Safari/537.36 Edge/15.15063";
            CookieContainer myCookieContainer2 = new CookieContainer();
            request.CookieContainer = myCookieContainer;

            buffer = new StringBuilder();
            buffer.AppendFormat("{0}={1}", "token", timestamp);
            buffer.AppendFormat("&{0}={1}", "nationPerfix", "86");
            buffer.AppendFormat("&{0}={1}", "login_email", "18621076121");
            buffer.AppendFormat("&{0}={1}", "login_pwd", "123456");
            buffer.AppendFormat("&{0}=", "csessionid1");
            buffer.AppendFormat("&{0}=", "sig1");
            buffer.AppendFormat("&{0}=", "alitoken1");
            buffer.AppendFormat("&{0}=", "scene1");
            requestEncoding = Encoding.GetEncoding("utf-8");
            data = requestEncoding.GetBytes(buffer.ToString());
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            response = request.GetResponse();
            cookieString = response.Headers["Set-Cookie"];
            Console.WriteLine(string.Format("2:{0}", cookieString));

            CookieContainer myCookieContainer1 = new CookieContainer();
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            request = WebRequest.Create(@"https://log.mmstat.com/eg.js") as HttpWebRequest;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "GET";
            request.Accept = "application/javascript, */*;q=0.8";
            request.Referer = "https://www.damai.cn/";
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Safari/537.36 Edge/15.15063";
            request.CookieContainer = myCookieContainer1;

            response = request.GetResponse();
            cookieString = response.Headers["Set-Cookie"];
            Console.WriteLine(string.Format("3:{0}", cookieString));        
        }
    }
}
