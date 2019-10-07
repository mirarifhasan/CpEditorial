using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CpEditorial.Models
{
    public class SearchModel
    {
        public string STitle { get; set; }

        public List<EditorialModel> searchList = new List<EditorialModel>();

        DBHelper dBHelper = new DBHelper();
        EditorialModel editorialModel = new EditorialModel();

        public SearchModel() { }
        public SearchModel(string sTitle)
        {
            string query = "select top 10 Editorial.EditorialID, Editorial.UpVote, Editorial.DownVote, Editorial.description, Problem.Title from Editorial, Problem, [User] where Editorial.ProblemID=Problem.ProblemID AND Editorial.UserID=[User].UserID and problem.title like '%" + sTitle + "%'";
            DataTable searchTable = new DBHelper().getTable(query);

            // Adding all rows in list
            for (int i = 0; i < searchTable.Rows.Count; i++)
            {
                editorialModel = new EditorialModel();
                editorialModel.EditorialID = Convert.ToInt32(searchTable.Rows[i][0].ToString());
                editorialModel.Description = searchTable.Rows[i][3].ToString();
                editorialModel.ProblemTitle = searchTable.Rows[i][4].ToString();

                searchList.Add(editorialModel);
            }
        }

        
        
    }
}