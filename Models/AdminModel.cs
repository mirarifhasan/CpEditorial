using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CpEditorial.Models
{
    public class AdminModel
    {
        public int Point { get; set; }

        public List<EditorialModel> approvalList = new List<EditorialModel>();

        public AdminModel(int userID)
        {
            string sql = "select Point from [User] where UserID=" + userID;
            DataTable dtbl = new DBHelper().getTable(sql);
            Point = Convert.ToInt32(dtbl.Rows[0][0].ToString());
            dtbl.Clear();

            sql = "select Editorial.EditorialID, Editorial.Solution, Problem.Title from Editorial, Problem where Editorial.ProblemID=Problem.ProblemID and Approve='No'";
            dtbl = new DBHelper().getTable(sql);

            EditorialModel editorialModel;
            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                editorialModel = new EditorialModel();
                editorialModel.EditorialID = Convert.ToInt32(dtbl.Rows[i][0].ToString());
                editorialModel.ProblemTitle = dtbl.Rows[i][2].ToString();
                editorialModel.Solution = dtbl.Rows[i][1].ToString();

                approvalList.Add(editorialModel);
            }

            
        }
        
    }
}