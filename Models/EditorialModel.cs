using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CpEditorial.Models
{
    public class EditorialModel
    {
        public int OJID { get; set; }
        public string OJName { get; set; }

        public int TagID { get; set; }
        public string TagName { get; set; }

        public int ProblemID { get; set; }
        public string ProblemTitle { get; set; }
        public string ProblemCode { get; set; }

        public int EditorialID { get; set; }
        public string DateOfPublishing { get; set; }
        public int UpVote { get; set; }
        public int DownVote { get; set; }
        public string Rephrase { get; set; }
        public string Solution { get; set; }
        public string Details { get; set; }

        public string MyVote { get; set; }
        public int BookMarkID { get; set; }

        public int UserID { get; set; }
        public string UserName { get; set; }
    }
}