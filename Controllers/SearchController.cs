using CpEditorial.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CpEditorial.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        [HttpGet]
        public ActionResult Index()
        {
            string sTitle = Request.QueryString["title"];
            if (sTitle == null)
            {
                SearchModel searchModel = new SearchModel();
                return View(searchModel);
            }
                
            else
            {
                SearchModel searchModel = new SearchModel(sTitle);
                ViewBag.Message = "No result found";
                return View(searchModel);
            }
        }
        
        [HttpPost]
        public RedirectResult Index(SearchModel searchModel)
        {
            return Redirect(Url.Content("~/Search/Index?title=" + searchModel.STitle));
        }
    }
}