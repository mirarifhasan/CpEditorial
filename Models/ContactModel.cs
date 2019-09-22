using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CpEditorial.Models
{
    public class ContactModel
    {
        public string name { get; set; }
        
        public string email { get; set; }
        
        public string message { get; set; }
    }
}