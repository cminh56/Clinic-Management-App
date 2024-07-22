using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ProjectClinicManagement.Models;
using MimeKit;
using MimeKit.Text;
using System.Security.Principal;

namespace ProjectClinicManagement.Helper
{
    public class SendEmail
    {
        public void sendEmailForgotPass(Account account,string password)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com"))
                {
                    mail.From = new MailAddress("1s2p3s0410@gmail.com");
                    mail.To.Add(account.Email);
                    mail.Subject = "New PassWord in ClinicManagement system";
                    var textPart = new TextPart(TextFormat.Html)
                    {
                        Text = $@"
                               Dear {account.Name},
                                This email is from ClinicManagement system,
                                Your password has been changed.:
                               UserName: {account.UserName}
                               New Password:  {password}
                                
                                If anything wrong, please reach out to clinicManagement@gmail.com. We are so sorry for this inconvenience.
                                Thanks & Regards!
                                ClinicManagement Team.
                            "
                    };

                    mail.Body = textPart.Text; // Set the HTML body from the TextPart
                    smtp.Port = 587;
                    smtp.Credentials = new NetworkCredential("1s2p3s0410@gmail.com", "kwpf jcam boaa vdmi");
                    smtp.EnableSsl = true;

                    smtp.Send(mail);
                    Console.WriteLine("Email sent successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        public void sendEmailCreateUser( Account account, string password)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com"))
                {
                    mail.From = new MailAddress("1s2p3s0410@gmail.com");
                    mail.To.Add(account.Email);
                    mail.Subject = "New Account ClinicManagement system";
                      var textPart = new TextPart(TextFormat.Html)
            {
                Text = $@"
                                Dear {account.Name},
                                This email is from ClinicManagement system,
                                Your account has been created. Please use the following credential to login:
                                
                                User name: {account.UserName}
                                Password:  {password}
                                
                                If anything wrong, please reach out to clinicManagement@gmail.com. We are so sorry for this inconvenience.
                                Thanks & Regards!
                                ClinicManagement Team.
                            "
            };

            mail.Body = textPart.Text; // Set the HTML body from the TextPart
                    smtp.Port = 587;
                    smtp.Credentials = new NetworkCredential("1s2p3s0410@gmail.com", "kwpf jcam boaa vdmi");
                    smtp.EnableSsl = true;

                    smtp.Send(mail);
                    Console.WriteLine("Email sent successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
  
}
