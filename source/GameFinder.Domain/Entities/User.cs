using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Domain.Entities
{
    public class User
    {
        [Key]
        public int UserId { get;  set; }
        public string Name { get;  set;}
        public string Surname { get;  set;}
        public DateTime Birthday { get;  set; }
        [ForeignKey("UserEmailAddress")]
        public string Email { get;  set;}
        public byte[] PasswordHash { get;  set;}
        public byte[] PasswordSalt { get; set; }
        public string Phone { get;  set;}
        public int RoleId { get;  set; }
        public virtual Role RoleRole { get;  set; }

    }
}
