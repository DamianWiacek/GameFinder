using GameFinder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Application.Models
{
    public class NewUserDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get;  set; }
        public DateTime Birthday { get;  set; }
        [Required,EmailAddress]
        public string Email { get;  set; }
        [Required]
        public string Password { get;  set; }
        [Required]
        public string Phone { get;  set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public string Pesel { get; set; }
    }
}
