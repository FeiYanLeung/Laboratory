using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Laboratory.NetworkTest
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Email
    {
        public void Send(params string[] toMailAddress)
        {
            using (var message = new MailMessage())
            {
                foreach (var address in toMailAddress)
                {
                    message.To.Add(new MailAddress(address));
                }

                message.From = new MailAddress("service@gu.com", "咕咕");
                message.Subject = "爱泼斯坦";
                message.SubjectEncoding = Encoding.Unicode;
                message.Body = "在校订作家埃德加·斯诺的一本书的过程中，爱泼斯坦和斯诺得以相互认识。斯诺在出版前给爱泼斯坦看了他后来的经典之作——《西行漫记》。";
                message.BodyEncoding = Encoding.Unicode;
                message.IsBodyHtml = false;

                var client = new SmtpClient("smtp.exmail.qq.com", 25)
                {
                    EnableSsl = false,
                    UseDefaultCredentials = true,
                    Credentials = new NetworkCredential("service@gu.com", "guxx2015")
                };

                try
                {
                    client.Send(message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
