using CpEditorial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CpEditorial.Controllers
{
    public class EditorialController : Controller
    {
        // GET: Post
        public ActionResult PostForm()
        {
            if(Session["UserID"] == null) return Content("<script language='javascript' type='text/javascript'>alert('Login to continue');</script>");

            PostFormModel postEditorialModel = new PostFormModel();
            return View(postEditorialModel); // Return all tag and OJ list in a big outer list
        }

        [HttpPost]
        public ActionResult AddEditorial(PostFormModel postFormModel)
        {
            postFormModel.DateTime = Convert.ToString(DateTime.UtcNow);
            postFormModel.UserID = Convert.ToInt32(Session["userID"]);
            postFormModel.Description = postFormModel.Rephrase + "</br>" + postFormModel.Solution + "\n\n" + postFormModel.Details;
            
            // Finding ProblemID 
            string subSql="";
            if (postFormModel.ProblemCode != null)
                subSql = " and Code='" + postFormModel.ProblemCode + "'";
            
            string sql = "select ProblemID from Problem where OJID = " + postFormModel.OJID + " and Title ='" + postFormModel.ProblemTitle+ "'" + subSql;
            var res = new DBHelper().getTable(sql);

            // If problemID already exist, otherwise 'else' part to generate problemID
            if (res.Rows.Count == 1)
                postFormModel.ProblemID = Convert.ToInt32(res.Rows[0][0]);
            else
            {
                if (postFormModel.ProblemCode == null) subSql = ", NULL";
                else subSql = ", '"+postFormModel.ProblemCode+"'";
                sql = "insert into Problem values ("+postFormModel.OJID+", '"+postFormModel.ProblemTitle+"'"+subSql+")";
                new DBHelper().setTable(sql);

                subSql = "";
                if (postFormModel.ProblemCode != null)
                    subSql = " and Code='" + postFormModel.ProblemCode + "'";

                sql = "select ProblemID from Problem where OJID = " + postFormModel.OJID + " and Title ='" + postFormModel.ProblemTitle + "'" + subSql;
                res = new DBHelper().getTable(sql);
                postFormModel.ProblemID = Convert.ToInt32(res.Rows[0][0]);
            }

            //Insert editorial in table
            sql = "insert into editorial values ("+postFormModel.UserID+", "+postFormModel.ProblemID+", "+postFormModel.TagID+", '"+postFormModel.Description+"', 0, 0, '"+postFormModel.DateTime+"')";
            new DBHelper().setTable(sql);


            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult ViewEditorial()
        {
            int editorialId = Convert.ToInt32(Request.QueryString["id"]);
            ViewEditorialModel viewEditorial = new ViewEditorialModel(editorialId);
            
            return View(viewEditorial);
        }
    }
}