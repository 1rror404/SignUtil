using HtmlAgilityPack;
using SignTest.API;
using SignTest.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
            doc.LoadHtml(DownloadStreamString(TieBaInterface.GetTieBa_URL));
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

        //GET
        public static string DownloadStreamString(string url)
        {
            WebClient wc = new WebClient();
            wc.Headers.Add("ContentType", "text/html;charset=UTF-8");
            wc.Headers.Add("User-Agent", TieBaInterface.PCUserAgent);
            wc.Headers.Add("Cookie",TieBaInterface.bduss);
            Stream objStream = wc.OpenRead(url);
            StreamReader read = new StreamReader(objStream, Encoding.GetEncoding("utf-8"));
            string str = read.ReadToEnd();
            objStream.Close();
            read.Close();
            return str;
        }
    }
}
