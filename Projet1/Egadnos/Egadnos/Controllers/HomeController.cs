using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Egadnos.Models;

/*/////////////////////////////////////////////////
 * 
 * ///////////////////////////////////////////////*/


namespace Egadnos.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        { 
            return View("Index");
        }

        public ActionResult CreaSondage()
        {
            return View("CreaSondage");
        }

       
    }
}