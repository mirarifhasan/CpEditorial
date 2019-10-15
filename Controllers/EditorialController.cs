using CpEditorial.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
            if (Session["UserID"] == null) return Content("<script language='javascript' type='text/javascript'>alert('Login to continue');</script>");

            int editorialId = 0;
            editorialId = Convert.ToInt32(Request.QueryString["eid"]);

            PostFormModel postEditorialModel;
            if (editorialId == 0)
            {
                postEditorialModel = new PostFormModel();
                Session["mode"] = "new";
                ViewBag.buttonName = "Submit";
            }
            else
            {
                postEditorialModel = new PostFormModel(editorialId);

                if (postEditorialModel.ProblemTitle == null) return RedirectToAction("Index", "Warning");

                Session["mode"] = "update";
                Session["eid"] = editorialId;
                ViewBag.buttonName = "Update";
            }

            return View(postEditorialModel); // Return all tag and OJ list in a big outer list
        }

        [HttpPost]
        public ActionResult AddEditorial(PostFormModel postFormModel)
        {
            if (Session["mode"].ToString() == "new")
            {
                postFormModel.DateOfPublishing = Convert.ToString(DateTime.UtcNow);
                postFormModel.UserID = Convert.ToInt32(Session["userID"]);

                // Finding ProblemID 
                getProblemID(postFormModel);

                //Insert editorial in table
                //string sql = "insert into editorial values (" + postFormModel.UserID + ", " + postFormModel.ProblemID + ", " + postFormModel.TagID + ", '" + postFormModel.Rephrase + "', '" + postFormModel.Solution + "', '" + postFormModel.Details + "', 0, 0, '" + postFormModel.DateOfPublishing + "')";
                string sql = "insert into editorial (userid, problemid, tagid, rephrase, solution, details) " +
                    "values (" + postFormModel.UserID + ", " +
                    postFormModel.ProblemID + ", " +
                    postFormModel.TagID + ", '" +
                    postFormModel.Rephrase + "', '" + postFormModel.Solution + "', '" + postFormModel.Details + "')";
                new DBHelper().setTable(sql);
            }
            else if (Session["mode"].ToString() == "update")
            {
                getProblemID(postFormModel);

                string sql = "update Editorial set ProblemID=" + postFormModel.ProblemID + ", TagID=" + postFormModel.TagID + ", Rephrase='" + postFormModel.Rephrase + "', Solution='" + postFormModel.Solution + "', Details='" + postFormModel.Details + "' where EditorialID=" + Session["eid"];
                Session.Remove("eid");
                new DBHelper().setTable(sql);
            }

            return RedirectToAction("Index", "Home");
        }

        protected void getProblemID(PostFormModel postFormModel)
        {
            // Finding ProblemID 
            string subSql = "";
            if (postFormModel.ProblemCode != null)
                subSql = " and ProblemCode='" + postFormModel.ProblemCode + "'";

            string sql = "select ProblemID from Problem where OJID = " + postFormModel.OJID + " and Title ='" + postFormModel.ProblemTitle + "'" + subSql;
            var res = new DBHelper().getTable(sql);

            // If problemID already exist, otherwise 'else' part to generate problemID
            if (res.Rows.Count == 1)
                postFormModel.ProblemID = Convert.ToInt32(res.Rows[0][0]);
            else
            {
                if (postFormModel.ProblemCode == null) subSql = ", NULL";
                else subSql = ", '" + postFormModel.ProblemCode + "'";
                sql = "insert into Problem values (" + postFormModel.OJID + ", '" + postFormModel.ProblemTitle + "'" + subSql + ")";
                new DBHelper().setTable(sql);

                subSql = "";
                if (postFormModel.ProblemCode != null)
                    subSql = " and ProblemCode='" + postFormModel.ProblemCode + "'";

                sql = "select ProblemID from Problem where OJID = " + postFormModel.OJID + " and Title ='" + postFormModel.ProblemTitle + "'" + subSql;
                res = new DBHelper().getTable(sql);
                postFormModel.ProblemID = Convert.ToInt32(res.Rows[0][0]);
            }
        }

        [HttpGet]
        public ActionResult ViewEditorial()
        {
            int editorialId = 0;
            editorialId = Convert.ToInt32(Request.QueryString["id"]);

            if (editorialId == 0) return Content("<script language='javascript' type='text/javascript'>alert('URL not correct');</script>");

            if (TempData["message"] != null) ViewBag.Error = TempData["message"];

            //Editorial existing check
            string sql = "select editorialid from editorial where editorialid="+editorialId+" and (approve='Yes' or userid="+Convert.ToInt32(Session["userID"])+")";
            DataTable dtbl = new DBHelper().getTable(sql);
            if (dtbl.Rows.Count == 0) return RedirectToAction("Index", "Warning");


            ViewEditorialModel viewEditorialModel;// = new ViewEditorialModel(editorialId);
            if (Session["userid"] == null)
            {
                viewEditorialModel = new ViewEditorialModel(editorialId);
            }
            else
            {
                viewEditorialModel = new ViewEditorialModel(editorialId, Convert.ToInt32(Session["userid"]));
            }
            return View(viewEditorialModel);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            string sql = "delete from EditorialTags where editorialID=" + id;
            new DBHelper().setTable(sql);
            sql = "delete from bookmark where editorialID=" + id;
            new DBHelper().setTable(sql);
            sql = "delete from Editorial where EditorialID=" + id;
            new DBHelper().setTable(sql);

            return Redirect("/Profile/Index?uid=" + Session["userID"]);
        }

        [HttpGet]
        public ActionResult Vote()
        {
            DBHelper dbHelper = new DBHelper();
            string voteType = Request.QueryString["v"];
            int eid = Convert.ToInt32(Request.QueryString["eid"]);
            string editorialWriterId = Request.QueryString["euid"];

            if (Session["userID"] == null)
            {
                TempData["message"] = "Login first";
                return Redirect("/Editorial/ViewEditorial?id=" + eid);
            }

            string sql;

            // Post owner can't vote on his own post
            sql = "select userid from editorial where editorialid=" + eid;
            DataTable dtbl = dbHelper.getTable(sql);

            if (dtbl.Rows.Count > 0 && Convert.ToInt32(dtbl.Rows[0][0].ToString()) == Convert.ToInt32(Session["userID"]))
            {
                TempData["message"] = "You can not vote on your own post";
                return Redirect("/Editorial/ViewEditorial?id=" + eid);
            }

            // Delete records if already voted but now want to change vote
            dtbl.Clear();
            sql = "select votetype from votetrack where userID=" + Session["userID"] + " and editorialID=" + eid;
            dtbl = dbHelper.getTable(sql);
            if (dtbl.Rows.Count > 0)
            {
                sql = "delete from votetrack where userID=" + Session["userID"] + " and editorialID=" + eid;
                dbHelper.setTable(sql);
                sql = "update editorial set " + Convert.ToString(dtbl.Rows[0][0]) + " = " + Convert.ToString(dtbl.Rows[0][0]) + "-1 where editorialID=" + eid;
                dbHelper.setTable(sql);
                if (Convert.ToString(dtbl.Rows[0][0]) == "upvote")
                {
                    sql = "update [user] set point=point-3 where userid=" + editorialWriterId;
                    dbHelper.setTable(sql);
                }
                else if (Convert.ToString(dtbl.Rows[0][0]) == "downvote")
                {
                    sql = "update [user] set point=point+2 where userid=" + editorialWriterId;
                    dbHelper.setTable(sql);
                }
            }


            // Adding new vote
            if (dtbl.Rows.Count == 0 || dtbl.Rows[0][0].ToString() != voteType)
            {
                sql = "update editorial set " + voteType + " = " + voteType + "+1 where editorialid=" + eid;
                dbHelper.setTable(sql);
                sql = "insert into votetrack values (" + Session["userID"] + ", " + eid + ", '" + voteType + "')";
                dbHelper.setTable(sql);
                if (voteType == "upvote")
                {
                    sql = "update [user] set point=point+3 where userid=" + editorialWriterId;
                }
                else if (voteType == "downvote")
                {
                    sql = "update [user] set point=point-2 where userid=" + editorialWriterId;
                }
                dbHelper.setTable(sql);
            }

            return Redirect("/Editorial/ViewEditorial?id=" + eid + "&userId=" + Convert.ToString(Session["userid"]));
        }

        [HttpPost]
        public ActionResult PostComment(string message)
        {
            if (Session["UserID"] == null)
                return Content("<script language='javascript' type='text/javascript'>alert('Login to continue');</script>");
            string command = "insert into comment (userid, editorialid, text) values (" +
                Session["UserID"] + ", " +
                Session["editorialId"] + ", '" + message + "')";
            new DBHelper().setTable(command);
            int eid = Convert.ToInt32(Session["editorialId"]);
            return Redirect("/Editorial/ViewEditorial?id=" + eid);
        }

        [HttpGet]
        public ActionResult PostReply()
        {
            int eid = Convert.ToInt32(Request.QueryString["eid"]);
            int uid = Convert.ToInt32(Request.QueryString["uid"]);
            int pid = Convert.ToInt32(Request.QueryString["pid"]);
            string text = Request.QueryString["text"];

            string sql = "INSERT INTO Comment (UserID, EditorialID, ParentId, Text) VALUES("+uid+","+eid+", "+pid+", '"+text+"');";
            new DBHelper().setTable(sql);

            return Redirect("/Editorial/ViewEditorial?id="+eid);
        }

        [HttpGet]
        public ActionResult RemoveBookmark(int id)
        {
            string sql = "select Userid from bookmark where bookmarkid=" + id;
            var dtbl = new DBHelper().getTable(sql);
            var uid = Convert.ToInt32(dtbl.Rows[0][0].ToString());

            if (uid != Convert.ToInt32(Session["userID"]))
                return Redirect("/Warning/Index");

            sql = "delete from bookmark where bookmarkid=" + id;
            new DBHelper().setTable(sql);

            return Redirect("/Profile/Index?uid=" + Session["userID"]);
        }

        [HttpGet]
        public ActionResult Approve(int id)
        {
            string sql = "update editorial set approve='Yes' where editorialid="+id;
            new DBHelper().setTable(sql);

            return Redirect("/Profile/Index?uid=" + Session["userID"]);
        }
    }
}
