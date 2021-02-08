using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Models.Account
{
    public class ApplicationUserIdentity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string NormalizedUsername { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public string PasswordHash { get; set; }
    }
}
