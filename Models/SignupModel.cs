using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CpEditorial.Models
{
    public class SignupModel
    {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        [Compare("password")]
        public string confirmPassword { get; set; }
    }
}