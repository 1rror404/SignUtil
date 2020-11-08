using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignTest.API
{
    class TieBaInterface
    {
        public static string bduss = "BDUSS=ByfnBiVkRHSjZYMTdBUTlVWnF5YlIyZkpLOENGaktkQ2ZCb3RQNmFkT2F2TDlmRVFBQUFBJCQAAAAAAAAAAAEAAAD-AFUAzvTI1bO~ueIAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAJovmF-aL5hfQV";
      
        public static string PCUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36";   //PC代理

        public static string GetTieBa_URL = "http://tieba.baidu.com/mo/m?tn=bdFBW&tab=favorite";             //获取用户关注的贴吧（老）
        public static string PCSignIn_URL = "http://tieba.baidu.com/sign/add";                               //PC端网页签到
        public static string ClientSign_URL = "http://c.tieba.baidu.com/c/c/forum/sign";                     //客户端签到
        public static string GetTbs_URL = "http://tieba.baidu.com/dc/common/tbs";                            //获取tbs
        public static string GetInfo_URL = "http://ce.baidu.com/site/getUserProfile";                       //获取用户名和头像
        public static string GetFid_URL = "http://tieba.baidu.com/f/commit/share/fnameShareApi?ie=utf-8&fname=";   //获取贴吧Fid
        public static string AddTieBa_URL = "http://c.tieba.baidu.com/c/c/forum/like";                        //关注一个贴吧（客户端）
        public static string RemoveTieBa_URL = "http://c.tieba.baidu.com/c/c/forum/unfavolike";              //取消关注一个贴吧 （客户端）
        public static string GetAllFans_URL = "http://c.tieba.baidu.com/c/u/fans/page";                     //获取粉丝

    }
}
