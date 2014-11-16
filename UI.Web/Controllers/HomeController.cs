using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Model;

namespace UI.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Board
        public ActionResult Index()
        {
            return View();
        }
    }
}