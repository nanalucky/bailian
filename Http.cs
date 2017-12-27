using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Security;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Threading;


namespace bailian
{
    class Player
    {
        public string strAccount = @"";
        public string strPassword = @"";
        public Thread thread;

        public void Run()
        {
            int nLoginTimes = 1;
            while(true)
            {
                Program.form1.UpdateDataGridView(strAccount, Column.Login, string.Format("开始登录:{0}", nLoginTimes));
                HttpWebRequest request = null;
                CookieContainer loginCookieContainer = new CookieContainer();

                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(Http.CheckValidationResult);
                request = WebRequest.Create(AllPlayers.strURL) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version11;
                request.Method = "GET";
                request.Accept = "text/html, application/xhtml+xml, image/jxr, */*";
                WebHeaderCollection headers = request.Headers;
                headers.Add("Accept-Language", "zh-Hans-CN,zh-Hans;q=0.5");
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36 Edge/16.16299";
                headers.Add("Accept-Encoding", "gzip, deflate");
                request.CookieContainer = loginCookieContainer;
                WebResponse response = request.GetResponse();
                //Console.WriteLine(string.Format("1:{0}\n", loginCookieContainer.ToString()));

                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(Http.CheckValidationResult);
                request = WebRequest.Create(string.Format(@"https://m.bl.com/h5-web/member/login.html?cacheFlag={0}", Http.Timestamp())) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version11;
                request.Method = "POST";
                headers = request.Headers;
                //headers.Add("Origin", "https://m.bl.com");
                //request.Referer = "https://m.bl.com/h5-web/member/view_login.html?redirctUrl=https%3A%2F%2Fm.bl.com%2Fh5-web%2Fseckill%2Fview_Login_Seckill.html%3FseckillFlag%3D1%26actTime%3DMS_2016122811150%26skuID%3D4271";
                headers.Add("Accept-Language", "zh-Hans-CN,zh-Hans;q=0.5");
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36 Edge/16.16299";
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.Accept = "*/*";
                headers.Add("X-Requested-With", "XMLHttpRequest");
                headers.Add("Accept-Encoding", "gzip, deflate, br");
                headers.Add("Cache-Control", "no-cache");
                request.CookieContainer = loginCookieContainer;
                StringBuilder buffer = new StringBuilder();
                buffer.AppendFormat("{0}={1}", "loginName", strAccount);
                buffer.AppendFormat("&{0}={1}", "password", Http.UserMd5(strPassword));
                buffer.AppendFormat("&{0}={1}", "type", "1");
                buffer.AppendFormat("&{0}={1}", "relocationRUL", Uri.EscapeDataString("https://m.bl.com/h5-web/seckill/view_Login_Seckill.html?seckillFlag=1&actTime=MS_2016122811150&skuID=4271"));
                buffer.AppendFormat("&{0}={1}", "mpFlag", "");
                Encoding requestEncoding = Encoding.GetEncoding("utf-8");
                Byte[] data = requestEncoding.GetBytes(buffer.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                response = request.GetResponse();
                System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream(), requestEncoding);
                string body = reader.ReadToEnd();
                if (body.IndexOf("success") >= 0)
                {
                    JObject joBody = (JObject)JsonConvert.DeserializeObject(body);
                    if (string.Compare((string)joBody["success"], "true", true) == 0)
                    {
                        Program.form1.UpdateDataGridView(strAccount, Column.Login, "成功");
                        break;
                    }
                }

                nLoginTimes++;
                if(nLoginTimes > 10)
                {
                    Program.form1.UpdateDataGridView(strAccount, Column.Login, "放弃");
                    break;
                }
            }
            if (nLoginTimes > 10)
                return;
        
    

        }


    };

    class AllPlayers
    {
        public static bool bSetProxy = false;
        public static string strURL = @"";
        public static DateTime dtStartTime;
        public static List<Player> listPlayer = new List<Player>();

        public static void Init()
        {
            Http.InitCnn();
            
            string szConfigFileName = System.Environment.CurrentDirectory + @"\" + @"config.txt";
            string szAccountFileName = System.Environment.CurrentDirectory + @"\" + @"account.txt";

            string[] arrayConfig = File.ReadAllLines(szConfigFileName);
            JObject joInfo = (JObject)JsonConvert.DeserializeObject(arrayConfig[0]);
            dtStartTime = DateTime.Parse((string)joInfo["StartTime"]);
            strURL = (string)joInfo["URL"];
            if ((string)joInfo["SetProxy"] == @"0")
                bSetProxy = false;
            else
                bSetProxy = true;
            Program.form1.Form1_Init();

            listPlayer = new List<Player>();
            string[] arrayText = File.ReadAllLines(szAccountFileName);
            for (int i = 1; i < arrayText.Length; ++i)
            {
                string[] arrayParam = arrayText[i].Split(new char[] { ',' });
                Player player = new Player();
                player.strAccount = arrayParam[0];
                player.strPassword = arrayParam[1];
                player.thread = new Thread(new ThreadStart(player.Run));
                listPlayer.Add(player);
                Program.form1.dataGridViewInfo_AddRow(arrayParam[0]);
            }
        }


        public static void Run()
        {
            foreach (Player player in listPlayer)
            {
                player.thread.Start();
            }

            foreach (Player player in listPlayer)
            {
                player.thread.Join();
            }

            Program.form1.richTextBoxStatus_AddString("任务完成!\n");
            Program.form1.button1_Enabled();
        }

        public static void Base64ToJPG()
        {
            string strBase64 = File.ReadAllText(System.Environment.CurrentDirectory + @"\" + @"base64.txt");
            Http.Base64StringToImage(System.Environment.CurrentDirectory + @"\" + @"pic", strBase64);
        }

        public static void ReadSecurityImage()
        {
            string strRet = Http.Cnn(System.Environment.CurrentDirectory + @"\" + @"pic.bmp");
            Console.WriteLine(@"CNN:" + strRet);
        }
    };
    
    
    
    class Http
    {
        [DllImport("OCR.dll")]
        public static extern StringBuilder CNN_OCR(int index, byte[] FileBuffer, int imglen, int zxd);
        [DllImport("OCR.dll")]
        public static extern int LCNN_INIT(string path, string ps, int threads);

        public static bool bInitCnn = false;

        public static void InitCnn()
        {
            if (!bInitCnn)
            {
                bInitCnn = true;
                LCNN_INIT(System.Environment.CurrentDirectory + @"\" + "字母数字.cnn", "", 100);
            }             
        }

        public static byte[] ImageToBytes(Image image)
        {
            ImageFormat format = image.RawFormat;
            using (MemoryStream ms = new MemoryStream())
            {
                if (format.Equals(ImageFormat.Jpeg))
                {
                    image.Save(ms, ImageFormat.Jpeg);
                }
                else if (format.Equals(ImageFormat.Png))
                {
                    image.Save(ms, ImageFormat.Png);
                }
                else if (format.Equals(ImageFormat.Bmp))
                {
                    image.Save(ms, ImageFormat.Bmp);
                }
                else if (format.Equals(ImageFormat.Gif))
                {
                    image.Save(ms, ImageFormat.Gif);
                }
                else if (format.Equals(ImageFormat.Icon))
                {
                    image.Save(ms, ImageFormat.Icon);
                }
                byte[] buffer = new byte[ms.Length];
                //Image.Save()会改变MemoryStream的Position，需要重新Seek到Begin
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        public static string Cnn(string imgPath)
        {
            Image img1 = Image.FromFile(imgPath);
            byte[] img = ImageToBytes(img1);
            StringBuilder sb = CNN_OCR(1, img, img.Length, 0);
            return sb.ToString();
        }
      
        
        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }

        public static string Timestamp()
        {
            TimeSpan span = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            return ((ulong)span.TotalMilliseconds).ToString();
        }

        public static string UserMd5(string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符
                pwd = pwd + s[i].ToString("x2");
            }
            return pwd;
        }


        /// base64编码的文本转为图片
        /// </summary>
        /// <param name="txtFileName">保存的路径加文件名</param>
        /// <param name="inputStr">要转换的文本</param>
        public static void Base64StringToImage(string txtFileName, string inputStr)
        {
            try
            {
                //  String inputStr = sr.ReadToEnd();
                byte[] arr = Convert.FromBase64String(inputStr);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);

                //bmp.Save(txtFileName + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                bmp.Save(txtFileName + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                //bmp.Save(txtFileName + ".gif", ImageFormat.Gif);
                //bmp.Save(txtFileName + ".png", ImageFormat.Png);
                ms.Close();
            }
            catch (Exception ex)
            {

            }
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
