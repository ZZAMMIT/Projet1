using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Egadnos.Models;
using System.Data.SqlClient;

/*////////////////////////////////////////////////////
 A modifier remplacement cosole par equival web asp
///////////////////////////////////////////////////*/

namespace Egadnos.Controllers
{
    public class CreationSondageController : Controller
    {
        struct CreationSondage
        {
           // public string TitreSondage;
            //public string DescriptifSondage;
            //public TypeSondage Choix;
        }

        enum TypeSondage
        {
            ChoixMultiple,
            ChoixUnique,
        }
        // GET: CreationSondage
        public ActionResult Index()
        {
       
            return View("Index");
        }
    }
}