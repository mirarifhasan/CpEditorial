using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CpEditorial.Models
{
    public class PostFormModel
    {
        public List<OJTagModel> tagList = new List<OJTagModel>();
        public List<OJTagModel> ojList = new List<OJTagModel>();
        
        // Constractor
        public PostFormModel()
        {
            DBHelper dBHelper = new DBHelper();
            OJTagModel ojTagModel = new OJTagModel();

            var tagTable = new DataTable();
            tagTable = dBHelper.getTable("SELECT * FROM Tag");

            var ojTable = new DataTable();
            ojTable = dBHelper.getTable("SELECT * FROM OnlineJudge");

            // Adding all tags in list
            for(int i=0; i<tagTable.Rows.Count; i++)
            {
                ojTagModel = new OJTagModel();
                ojTagModel.id = Convert.ToInt32(tagTable.Rows[i][0].ToString());
                ojTagModel.value = tagTable.Rows[i][1].ToString();

                tagList.Add(ojTagModel);
            }
            // Adding all OJ names in list
            for (int i = 0; i < ojTable.Rows.Count; i++)
            {
                ojTagModel = new OJTagModel();
                ojTagModel.id = Convert.ToInt32(ojTable.Rows[i][0].ToString());
                ojTagModel.value = ojTable.Rows[i][1].ToString();

                ojList.Add(ojTagModel);
            }
        }

        // Form fields
        public string problemName { get; set; } 
        public string problemUrl { get; set; } 
        public int ojId { get; set; } 
        public int tagId { get; set; } 
        public string rephrase { get; set; } 
        public string solution { get; set; } 
        public string details { get; set; } 

    }
}