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
        public List<EditorialModel> pendingEditorialList = new List<EditorialModel>();
        public List<EditorialModel> InvolvmentEditorialList = new List<EditorialModel>();

        public ProfileModel(int userID)
        {
            string sql = "select Point from [User] where UserID="+userID;
            DataTable dtbl = new DBHelper().getTable(sql);
            Point = Convert.ToInt32(dtbl.Rows[0][0].ToString());
            dtbl.Clear();

            sql = "select Editorial.EditorialID, Editorial.Solution, Problem.Title from Editorial, Problem where editorial.approve='Yes' and Editorial.UserID="+userID+" and Editorial.ProblemID=Problem.ProblemID";
            dtbl = new DBHelper().getTable(sql);

            EditorialModel editorialModel;
            for(int i=0; i<dtbl.Rows.Count; i++)
            {
                editorialModel = new EditorialModel();
                editorialModel.EditorialID = Convert.ToInt32(dtbl.Rows[i][0].ToString());
                editorialModel.ProblemTitle = dtbl.Rows[i][2].ToString();
                editorialModel.Solution = dtbl.Rows[i][1].ToString();

                myEditorialList.Add(editorialModel);
            }


            sql = "select bookmark.bookmarkid, Bookmark.EditorialID, Editorial.Solution, Problem.Title from Editorial, Problem, Bookmark where Bookmark.UserID="+userID+" and Editorial.EditorialID=Bookmark.EditorialID and Editorial.ProblemID=Problem.ProblemID";
            dtbl.Clear();
            dtbl = new DBHelper().getTable(sql);
            
            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                editorialModel = new EditorialModel();
                editorialModel.EditorialID = Convert.ToInt32(dtbl.Rows[i][1].ToString());
                editorialModel.ProblemTitle = dtbl.Rows[i][3].ToString();
                editorialModel.Solution = dtbl.Rows[i][2].ToString();
                editorialModel.BookMarkID = Convert.ToInt32(dtbl.Rows[i][0].ToString());

                bookmarkList.Add(editorialModel);
            }


            sql = "select Editorial.EditorialID, Editorial.Solution, Problem.Title from Editorial, Problem where Editorial.EditorialID in (select distinct EditorialID from Comment where UserID="+userID+") and Editorial.ProblemID=Problem.ProblemID";
            dtbl.Clear();
            dtbl = new DBHelper().getTable(sql);

            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                editorialModel = new EditorialModel();
                editorialModel.EditorialID = Convert.ToInt32(dtbl.Rows[i][0].ToString());
                editorialModel.ProblemTitle = dtbl.Rows[i][2].ToString();
                editorialModel.Solution = dtbl.Rows[i][1].ToString();

                InvolvmentEditorialList.Add(editorialModel);
            }


            sql = "select Editorial.EditorialID, Editorial.Solution, Problem.Title from Editorial, Problem where editorial.approve='No' and Editorial.UserID=" + userID + " and Editorial.ProblemID=Problem.ProblemID";
            dtbl.Clear();
            dtbl = new DBHelper().getTable(sql);

            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                editorialModel = new EditorialModel();
                editorialModel.EditorialID = Convert.ToInt32(dtbl.Rows[i][0].ToString());
                editorialModel.ProblemTitle = dtbl.Rows[i][2].ToString();
                editorialModel.Solution = dtbl.Rows[i][1].ToString();

                pendingEditorialList.Add(editorialModel);
            }


        }

    }
}