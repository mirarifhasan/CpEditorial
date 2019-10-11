using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CpEditorial.Models
{
    public class PostFormModel : EditorialModel
    {
        public List<OJTagModel> tagList = new List<OJTagModel>();
        public List<OJTagModel> ojList = new List<OJTagModel>();

        // Constractor
        public PostFormModel()
        {
            loadList();
        }

        public PostFormModel(int editorialId)
        {
            loadList();

            string sql = "select * from editorial, [user], Problem, OnlineJudge, Tag where Editorial.EditorialID="+ editorialId + " and Editorial.TagID=Tag.TagID and Editorial.ProblemID=Problem.ProblemID and Editorial.UserID=[User].UserID and Problem.OJID=OnlineJudge.OJID";
            DataTable dtbl = new DBHelper().getTable(sql);

            EditorialID = editorialId;
            ProblemTitle = dtbl.Rows[0][18].ToString();
            OJID = Convert.ToInt32(dtbl.Rows[0][17].ToString());
            TagID = Convert.ToInt32(dtbl.Rows[0][3].ToString());
            Rephrase = dtbl.Rows[0][4].ToString();
            Solution = dtbl.Rows[0][5].ToString();
            Details = dtbl.Rows[0][6].ToString();
        }

        private void loadList()
        {
            DBHelper dBHelper = new DBHelper();
            OJTagModel ojTagModel = new OJTagModel();

            var tagTable = new DataTable();
            tagTable = dBHelper.getTable("SELECT * FROM Tag");

            var ojTable = new DataTable();
            ojTable = dBHelper.getTable("SELECT * FROM OnlineJudge");

            // Adding all tags in list
            for (int i = 0; i < tagTable.Rows.Count; i++)
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
    }
}