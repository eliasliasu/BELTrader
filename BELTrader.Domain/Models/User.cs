using System;
using System.Collections.Generic;
using System.Text;

namespace BELTrader.Domain.Models
{
    public class User : DomainObject
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public DateTime DateJoined { get; set; }
    }
}
