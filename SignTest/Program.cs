using SignTest.Sign;
using SignTest.API;
using SignTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SignTest
{
    class Program
    {
        public static string bduss = "";

        static void Main(string[] args)
        {
            //List<MyTieBa> tiebaList = new List<MyTieBa>();
            //tiebaList = TieBaHelper.GetUserTieBa(bduss);
            //for (int i = 0; i < tiebaList.Count; i++)
            //{
            //    Console.WriteLine("吧名：" + tiebaList[i].TBName + "\t" + tiebaList[i].TBLV + "\t经验：" + tiebaList[i].TBEXP);
            //}
            //Console.WriteLine("一共" + tiebaList.Count + "个吧");


            //string result = TieBaHelper.PCSignIn(bduss);


            //string tieBaName = "abc";
            //string siginResult = TieBaHelper.ClientSignTest(TieBaHelper.GetFid(tieBaName), tieBaName,bduss);
            //Console.WriteLine(siginResult);


            //TieBaHelper.SignAllTieBa(bduss);


            //string tiebaname = "测试";
            //string result = TieBaHelper.AddTieBa(TieBaHelper.GetFid(tiebaname), tiebaname, bduss);
            //Console.WriteLine(result);


            //string tiebaname = "测试";
            //string result = TieBaHelper.RemoveTieBa(tiebaname, bduss);
            //Console.WriteLine(result);

            Test();

         
        
        }

        public static void Test()
        {
            List<String> bdussList = new List<String>();
           
            bdussList.Add("BDUSS=dVdi15YWJGaXVsay1mWH52blNTWE5-MDkycS1VNDJoeENSMVYxNlNQdXpNazlmSVFBQUFBJCQAAAAAAAAAAAEAAAC2-rKb17fDzrqitMoAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAALOlJ1-zpSdfbm");
            for (int i = 0; i < bdussList.Count; i++)
            {
                if (TieBaHelper.isExist(bdussList[i]) == "1")
                {
                  
                    TieBaHelper.SignAllTieBa(bdussList[i]);
                    Console.WriteLine("**************************************签到完毕**************************************");
                  //  SendEmail.SignEmail("", "3024@163.com");

                }
                else
                {
                    Console.WriteLine("BDUSS无效");
                }
            }
        }

    }
}
