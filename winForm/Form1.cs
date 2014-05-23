using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        async private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
                return;

            dynamic data = e.Data.GetData(DataFormats.FileDrop);
            tbFile.Text = data[0];

            await SendMail(
                tbSmtp.Text,
                tbLogin.Text,
                tbPassword.Text,
                tbFrom.Text,
                tbTo.Text,
                "convert",
                String.Empty,
                tbFile.Text
                );

            MessageBox.Show("Finished");
        }

        async private Task SendMail(
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
            await smtp.SendMailAsync(mail);
        }

        private void Form1_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.None;
            else
                e.Effect = DragDropEffects.Copy;
        }
    }
}
