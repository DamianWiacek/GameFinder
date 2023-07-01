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
    public class Notification
    {
        public int NotificationId { get; private set; }
        
        public string Subject { get; private set; }
        public string Message { get; private set; }
        public bool IsSent { get; private set; }
        public DateTime Created { get; private set; }
        public string UserEmailAddress { get; private set; }  

        public int UserInboxId { get; private set; }
        public virtual UserInbox UserInbox { get; private set; }



        public Notification(int userInboxId, string subject, string message)
        {           
            UserInboxId= userInboxId;
            Created = DateTime.Now;
            Subject = subject;
            IsSent = false;
            Message = message;
            
        }
        public async Task SendThroughEmail()
        {
            //ToBeImplemented.. :)
            
        }
    }
}
