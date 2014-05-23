using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace kindleSender
{
    class Program
    {
        static string username = "";
        static string password = "";
        static string fromaddress = "";
        static string toaddress = "";
        static string smtpserver = "";

        static void Main(string[] args)
        {
            SendMail("smtp.163.com",
                "john.guoyichao",
                "",
                "john.guoyichao@163.com",
                "john.guoyichao@kindle.cn",
                "convert",
                "",
                @"");
        }

        static void SendMail(
            string smtpserver,
            string username, 
            string password,
            string fromaddress, 
            string toaddress, 
            string subject, 
            string content, 
            string filename)
        {
            MailAddress from = new MailAddress(fromaddress);
            MailAddress to = new MailAddress(toaddress);
            MailMessage mail = new MailMessage(from, to);
            Attachment att = new Attachment(filename);

            mail.Subject = subject;
            mail.Body = content;
            mail.Attachments.Add(att);

            var smtp = new SmtpClient(smtpserver);
            smtp.Credentials = new NetworkCredential(username, password);
            smtp.DeliveryFormat = SmtpDeliveryFormat.International;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(mail);
        }
    }
}
