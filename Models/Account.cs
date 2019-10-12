using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CpEditorial.Models
{
    public class Account
    {
        public string UserName { get; set; }
        public int Point { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }


        public List<Account> UserInfo { get; set; }
    }
}