using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CpEditorial.Models
{
    public class ViewEditorialModel
    {
        DbSchema dSchema = new DbSchema();

        public Editorial editorial { get; set; }
        public Problem problem { get; set; }
        public EditorialTags editorialTags { get; set; }
        public User user { get; set; }
        public OnlineJudge onlineJudge { get; set; }
        public Comment comment { get; set; }
        public List<Comment> commentList { get; }
        public List<List<Comment>> replyList { get; set; }
        public ViewEditorialModel(int editorialID)
        {
            editorial = dSchema.GetEditorial(editorialID);
            problem = dSchema.GetProblem(editorial.problemId);
            editorialTags = dSchema.GetEditorialTags(editorial.editorialId);
            user = dSchema.GetUser(editorial.userId);
            onlineJudge = dSchema.GetOnlineJudge(problem.ojId);
            comment = new Comment();

            commentList = dSchema.GetCommentsOfEditorial(editorial.editorialId);
            replyList = new List<List<Comment>>();
            foreach (Comment c in commentList)
            {
                try
                {
                    replyList.Add(dSchema.GetRepliesOfComment(c.commentId));
                }
                catch (System.NullReferenceException)
                {
                    replyList.Add(new List<Comment>());
                }
            }
        }
    }
    //public class ViewEditorialModel : EditorialModel
    //{
    //    DataTable ediTbl = new DataTable();
    //    public ViewEditorialModel(int editorialID)
    //    {
    //        string sql = "select * from Editorial, [User], Tag, Problem, OnlineJudge where Editorial.EditorialID=" + editorialID + " and Editorial.UserID=[User].UserID and Editorial.TagID=Tag.TagID and Editorial.ProblemID=Problem.ProblemID and Problem.OJID=OnlineJudge.OJID";
    //        ediTbl = new DBHelper().getTable(sql);

    //        ProblemTitle = Convert.ToString(ediTbl.Rows[0][20]);
    //        Rephrase = Convert.ToString(ediTbl.Rows[0][4]);
    //        Solution = Convert.ToString(ediTbl.Rows[0][5]);
    //        Details = Convert.ToString(ediTbl.Rows[0][6]);
    //        TagName = Convert.ToString(ediTbl.Rows[0][17]);
    //        UserName = Convert.ToString(ediTbl.Rows[0][11]);
    //        TagID = Convert.ToInt32(ediTbl.Rows[0][3]);
    //        DateOfPublishing = Convert.ToString(ediTbl.Rows[0][9]);
    //    }
    //}
}
