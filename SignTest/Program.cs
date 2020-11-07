using SignTest.Sign;
using SignTest.API;
using SignTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<MyTieBa> tiebaList = new List<MyTieBa>();
            tiebaList = TieBaHelper.GetUserTieBa(TieBaInterface.bduss);

            for (int i = 0; i < tiebaList.Count; i++)
            {
                Console.WriteLine("吧名：" + tiebaList[i].TBName + "\t" + tiebaList[i].TBLV + "\t经验：" + tiebaList[i].TBEXP);
            }
            Console.WriteLine("一共" + tiebaList.Count + "个吧");

            // string result = TieBaHelper.PCSignIn(TieBaInterface.bduss);

            //string tieBaName="波波维奇";
            //string siginResult = TieBaHelper.ClientSignTest(TieBaHelper.GetFid(tieBaName),tieBaName,TieBaInterface.bduss);
            //Console.WriteLine(siginResult);

            //TieBaHelper.SignAllTieBa(TieBaInterface.bduss);


        }
    }
}
