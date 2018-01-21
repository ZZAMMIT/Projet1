using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MonApplication.Models;
using System.Data.SqlClient;

namespace Egadnos.Controllers
{
    public class CreationSondageController : Controller
    {
        struct CreationSondage
        {
            public string TitreSondage;
            public string DescriptifSondage;
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
            CreationSondage sondage = new CreationSondage();
            Console.WriteLine("Titre sondage");
            sondage.TitreSondage = Console.ReadLine();
            Console.WriteLine("Descrption sondage");
            sondage.DescriptifSondage = Console.ReadLine();

            Console.WriteLine("Type sondage: Multiréponse ? ");
            string reponse = Console.ReadLine();
   
                

            switch(reponse)
            {
                case "oui":
                    Console.WriteLine("Vous avez choisi : " + TypeSondage.ChoixMultiple);
                    break;
                case "Non":
                    Console.WriteLine("Vous avez choisi : " + TypeSondage.ChoixUnique);
                    break;
            }


            return View();
        }
    }
}