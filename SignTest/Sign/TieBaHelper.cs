using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SignTest.API;
using SignTest.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SignTest.Sign
{
    class TieBaHelper
    {
        //获取用户关注的贴吧
        public static List<MyTieBa> GetUserTieBa(string bduss)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(DownloadStreamString(TieBaInterface.GetTieBa_URL,bduss));
            HtmlNode documentNode = doc.DocumentNode;
            List<MyTieBa> TieBaList = new List<MyTieBa>();
            try
            {
                HtmlNode table = doc.DocumentNode.SelectSingleNode("/html/body/div/div[2]/table");  ///获取table节点  
                for (int i = 1; i < table.ChildNodes.Count() + 1; i++)                             //table.ChildNodes.Count() table子节点的数量
                {
                    string tiebaname = documentNode.SelectSingleNode("/html/body/div/div[2]/table/tr[" + i + "]/td[1]/a").InnerText.Trim();
                    string tiebalv = documentNode.SelectSingleNode("/html/body/div/div[2]/table/tr[" + i + "]/td[2]").InnerText.Trim();
                    string tiebaexp = documentNode.SelectSingleNode("/html/body/div/div[2]/table/tr[" + i + "]/td[3]").InnerText.Trim();
                    MyTieBa myTieBa = new MyTieBa();
                    myTieBa.TBName = tiebaname;
                    myTieBa.TBLV = tiebalv;
                    myTieBa.TBEXP = tiebaexp;
                    TieBaList.Add(myTieBa);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("获取贴吧有误");
                throw;
            }

            return TieBaList;
        }


        //签到所有吧
        public static void SignAllTieBa(string bduss)
        {
            List<MyTieBa> tiebaList = TieBaHelper.GetUserTieBa(bduss);
            for (int i = 0; i < tiebaList.Count; i++)
            {
                string fid = TieBaHelper.GetFid(tiebaList[i].TBName);
                JObject result = (JObject)JsonConvert.DeserializeObject(TieBaHelper.ClientSignTest(fid, tiebaList[i].TBName, bduss));
                string code = result["error_code"].ToString();
                if (code == "0")
                {
                    Console.WriteLine(tiebaList[i].TBName + ":签到成功");
                }
                else
                {
                    string msg = result["error_msg"].ToString();
                    Console.WriteLine(tiebaList[i].TBName + ":" + msg);
                }

            }
        }

        //客户端签到
        public static string ClientSignTest(string fid, string kw, string bduss)
        {
            Dictionary<string, string> postDic = new Dictionary<string, string>();
            postDic.Add("BDUSS", bduss.Replace("BDUSS=", ""));
            postDic.Add("_client_id", "03-00-DA-59-05-00-72-96-06-00-01-00-04-00-4C-43-01-00-34-F4-02-00-BC-25-09-00-4E-36");
            postDic.Add("_client_type", "4");
            postDic.Add("_client_version", "1.2.1.17");
            postDic.Add("_phone_imei", "540b43b59d21b7a4824e1fd31b08e9a6");
            postDic.Add("fid", fid);
            postDic.Add("kw", kw);
            postDic.Add("net_type", "3");
            postDic.Add("tbs", GetTbs(bduss));
            postDic.Add("sign", Tran(postDic));

            return SignInPost(TieBaInterface.ClientSign_URL, postDic, bduss).ToString();
        }

        //PC端网页签到
        public static string PCSignIn(string bduss)
        {
            string result = string.Empty;
            List<MyTieBa> tiebalist = GetUserTieBa(bduss);
            for (int i = 0; i < tiebalist.Count; i++)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("ie", "utf-8");
                dic.Add("kw", tiebalist[i].TBName);
                dic.Add("tbs", GetTbs(bduss));
                JObject Jresult = SignInPost(TieBaInterface.PCSignIn_URL, dic, bduss);
                result = Jresult["no"].ToString();

            }
            return result;
        }

        //关注贴吧(客户端)
        public static string AddTieBa(string fid, string kw, string bduss)
        {
            string result = string.Empty;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("BDUSS", bduss.Replace("BDUSS=", ""));
            dic.Add("fid", fid);
            dic.Add("kw", kw);
            dic.Add("tbs", GetTbs(bduss));
            dic.Add("sign", Tran(dic));
            JObject Jresult = SignInPost(TieBaInterface.AddTieBa_URL, dic, bduss);
            result = Jresult["error_code"].ToString();
            return result;
        }

        //取消关注贴吧(客户端)
        public static string RemoveTieBa(string kw, string bduss)
        {
            string result = string.Empty;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("BDUSS", bduss.Replace("BDUSS=", ""));
            dic.Add("fid", GetFid(kw));
            dic.Add("kw", kw);
            dic.Add("tbs", GetTbs(bduss));
            dic.Add("sign", Tran(dic));
            JObject Jresult = SignInPost(TieBaInterface.RemoveTieBa_URL, dic, bduss);
            result = Jresult["error_code"].ToString();
            return result;
        }


        //获取用户名和头像
        public static string GetUserInfo(string bduss)
        {
            try
            {
                JObject JInfo = (JObject)JsonConvert.DeserializeObject(DownloadStreamString(TieBaInterface.GetInfo_URL,bduss));
                string code = JInfo["code"].ToString();
                string username = JInfo["data"]["username"].ToString();
                string img = JInfo["data"]["portrait"].ToString();
                if (code == "0")
                {
                    return username;
                }
                else
                {
                    return "1";
                }
            }
            catch (Exception)
            {
                return "1";
                throw;
            }

        }


        //获取贴吧的fid
        public static string GetFid(string name)
        {
            JObject JData = (JObject)JsonConvert.DeserializeObject(DownloadStreamString(TieBaInterface.GetFid_URL + name,""));
            string fid = JData["data"]["fid"].ToString();
            return fid;
        }

        //获取tbs
        public static string GetTbs(string bduss)
        {
            WebClient client = new WebClient();
            client.Headers.Add("Cookie", bduss);
            Stream st = client.OpenRead(TieBaInterface.GetTbs_URL);
            StreamReader sr = new StreamReader(st);
            string res = sr.ReadToEnd();
            sr.Close();
            st.Close();
            JObject Jres = (JObject)JsonConvert.DeserializeObject(res);
            return Jres["tbs"].ToString();
        }

        //判断BDUSS是否有效
        public static string isExist(string bduss) {

            WebClient client = new WebClient();
            client.Headers.Add("Cookie", bduss);
            Stream st = client.OpenRead(TieBaInterface.GetTbs_URL);
            StreamReader sr = new StreamReader(st);
            string res = sr.ReadToEnd();
            sr.Close();
            st.Close();
            JObject Jres = (JObject)JsonConvert.DeserializeObject(res);
            return Jres["is_login"].ToString();
        }

        //客户端Sign加密转换
        public static string Tran(Dictionary<string, string> dic)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in dic)
            {
                sb.AppendFormat("{0}={1}", item.Key, item.Value);
            }
            sb.Append("tiebaclient!!!");
            return Md5(sb.ToString()).ToUpper();
        }

        //MD5加密字符串
        public static string Md5(string input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            var data = Encoding.UTF8.GetBytes(input);
            var md5Data = md5.ComputeHash(data);
            md5.Clear();
            return md5Data.Aggregate(string.Empty, (current, t) => current + t.ToString(@"x2").PadLeft(2, '0'));
        }

        //POST
        public static JObject SignInPost(string url, Dictionary<string, string> dic, string bduss)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/json;charset=UTF-8";
            req.Headers.Add("Cookie", bduss);
            #region 添加Post 参数
            StringBuilder builder = new StringBuilder();
            int i = 0;
            foreach (var item in dic)
            {
                if (i > 0)
                    builder.Append("&");
                builder.AppendFormat("{0}={1}", item.Key, item.Value);
                i++;
            }
            byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            #endregion
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return (JObject)JsonConvert.DeserializeObject(result);
        }

        //GET
        public static string DownloadStreamString(string url,string bduss)
        {
            WebClient wc = new WebClient();
            wc.Headers.Add("ContentType", "text/html;charset=UTF-8");
            wc.Headers.Add("User-Agent", TieBaInterface.PCUserAgent);
            wc.Headers.Add("Cookie", bduss);
            Stream objStream = wc.OpenRead(url);
            StreamReader read = new StreamReader(objStream, Encoding.GetEncoding("utf-8"));
            string str = read.ReadToEnd();
            objStream.Close();
            read.Close();
            return str;
        }

    }
}
