using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameFinder.Domain.Entities
{
    public class EMail
    {
        public int EmailId { get; private set; }
        public string UserEmailAddress { get; private set; }
        public string Subject { get; private set; }
        public string Message { get; private set; }
        public bool IsSent { get; set; }



        public EMail(string userEmailAddress, string subject, string message)
        {           
            Regex regex = new Regex("(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])");
            if(!regex.IsMatch(userEmailAddress)) throw new ArgumentException("Wrong email address!");
            
            UserEmailAddress = userEmailAddress;
            Subject = subject;
            IsSent = false;
            Message = message;
            
        }
        public async Task Send()
        {
            try 
            { 
            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("admin@GameFinder.com","GameFinder Admin");
            mailMessage.To.Add(new MailAddress(UserEmailAddress));
            mailMessage.Subject = Subject;
            mailMessage.Body = Message;
            /*
            using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {             
                smtpClient.Credentials = new NetworkCredential("user", "password");
                smtpClient.EnableSsl = true;
                smtpClient.SendAsync(mailMessage,null);
                
            }
            */
            }
            finally { 
            IsSent = true;
            }
        }
    }
}
