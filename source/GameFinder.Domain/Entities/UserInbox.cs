using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Domain.Entities
{
    public class UserInbox
    {
        public int UserInboxId { get; private set; }

        public int UserId { get; private set; }
       
        public virtual List<Notification> Notifications { get; private set; }
    }
}
