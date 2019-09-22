using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CpEditorial.Models
{
    public class LoginModel
    {
        public string userID { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int point { get; set; }
        public string type { get; set; }
    }
}