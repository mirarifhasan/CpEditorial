using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CpEditorial.Models
{
    public class ProfileModel
    {
        public int Point { get; set; }

        public List<EditorialModel> bookmarkList = new List<EditorialModel>();
        public List<EditorialModel> myEditorialList = new List<EditorialModel>();
        public List<EditorialModel> InvolvmentEditorialList = new List<EditorialModel>();

        public ProfileModel(int userID)
        {
            string sql = "select Editorial.EditorialID, Editorial.Description, Problem.Title from Editorial, Problem where Editorial.UserID="+userID+" and Editorial.ProblemID=Problem.ProblemID";
            DataTable dtbl = new DBHelper().getTable(sql);

            EditorialModel editorialModel;
            for(int i=0; i<dtbl.Rows.Count; i++)
            {
                editorialModel = new EditorialModel();
                editorialModel.EditorialID = Convert.ToInt32(dtbl.Rows[i][0].ToString());
                editorialModel.ProblemTitle = dtbl.Rows[i][2].ToString();
                editorialModel.Description = dtbl.Rows[i][1].ToString();

                myEditorialList.Add(editorialModel);
            }


            sql = "select Bookmark.EditorialID, Editorial.Description, Problem.Title from Editorial, Problem, Bookmark where Bookmark.UserID="+userID+" and Editorial.EditorialID=Bookmark.EditorialID and Editorial.ProblemID=Problem.ProblemID";
            dtbl.Clear();
            dtbl = new DBHelper().getTable(sql);
            
            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                editorialModel = new EditorialModel();
                editorialModel.EditorialID = Convert.ToInt32(dtbl.Rows[i][0].ToString());
                editorialModel.ProblemTitle = dtbl.Rows[i][2].ToString();
                editorialModel.Description = dtbl.Rows[i][1].ToString();

                bookmarkList.Add(editorialModel);
            }


            sql = "select Editorial.EditorialID, Editorial.Description, Problem.Title from Editorial, Problem, Comment where Comment.UserID="+userID+" and Editorial.EditorialID=Comment.EditorialID and Editorial.ProblemID=Problem.ProblemID";
            dtbl.Clear();
            dtbl = new DBHelper().getTable(sql);

            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                editorialModel = new EditorialModel();
                editorialModel.EditorialID = Convert.ToInt32(dtbl.Rows[i][0].ToString());
                editorialModel.ProblemTitle = dtbl.Rows[i][2].ToString();
                editorialModel.Description = dtbl.Rows[i][1].ToString();

                InvolvmentEditorialList.Add(editorialModel);
            }

            
        }

    }
}