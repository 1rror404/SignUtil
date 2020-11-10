using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SignTest.API;

namespace SignTest.Sign
{
    class SendEmail
    {
        public static void SignEmail(string username, string useremail)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.qq.com", 587);
                MailMessage msg = new MailMessage(TieBaInterface.AdminEmail, useremail, "云签通知", "尊敬的用户：" + username + "，您的贴吧账号已签到完毕！");
                client.UseDefaultCredentials = false;
                NetworkCredential basicAuthenticationInfo = new NetworkCredential(TieBaInterface.AdminEmail, "");
                client.Credentials = basicAuthenticationInfo;
                client.EnableSsl = true;
                client.Send(msg);
            }
            catch (Exception)
            {

                Console.WriteLine("邮箱发送失败！");
                throw;
            }
           
        }
    }
}
