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

            int editorialId = 0;
            editorialId = Convert.ToInt32(Request.QueryString["eid"]);

            PostFormModel postEditorialModel;
            if (editorialId == 0) {
                postEditorialModel = new PostFormModel();
                ViewBag.buttonName = "Submit";
            }
            else {
                postEditorialModel = new PostFormModel(editorialId);
                ViewBag.buttonName = "Update";
            }

            return View(postEditorialModel); // Return all tag and OJ list in a big outer list
        }

        [HttpPost]
        public ActionResult AddEditorial(PostFormModel postFormModel)
        {
            postFormModel.DateOfPublishing = Convert.ToString(DateTime.UtcNow);
            postFormModel.UserID = Convert.ToInt32(Session["userID"]);
            
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
            sql = "insert into editorial values ("+postFormModel.UserID+", "+postFormModel.ProblemID+", "+postFormModel.TagID+", '"+postFormModel.Rephrase+"', '" +postFormModel.Solution+ "', '"+postFormModel.Details+"', 0, 0, '"+postFormModel.DateOfPublishing+"')";
            new DBHelper().setTable(sql);


            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult ViewEditorial()
        {
            int editorialId = 0;
            editorialId = Convert.ToInt32(Request.QueryString["id"]);

            if (editorialId == 0) return Content("<script language='javascript' type='text/javascript'>alert('URL not correct');</script>");

            ViewEditorialModel viewEditorialModel = new ViewEditorialModel(editorialId);
            return View(viewEditorialModel);
        }
    }
}