using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Egadnos.Models;

/*/////////////////////////////////////////////////
 * Page requetes SQL a verifier 
 * ///////////////////////////////////////////////*/


namespace Egadnos.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            PageIndexModel model = new PageIndexModel();
            model.CompteurVote = RecupererNombreVotesDepuisBDD();
            return View(Model1);
        }
        
        private const string SqlConnectionString =
        @"Server=.\SQLExpress;Initial Catalog=Geandos; Trusted_Connection=Yes";

        //Récupération du nombre de votes 
        private int RecupererNombreVotesDepuisBDD()
        {
            SqlConnection connexion = new SqlConnection(SqlConnectionString);
            connexion.Open();

            SqlCommand recuperationNombreVotes =
                new SqlCommand("SELECT COUNT(NumReponse) FROM ReponsePossible", connexion);

            int nombreVotes = (int)recuperationNombreVotes.ExecuteScalar();

            connexion.Close();

            return nombreVotes;
        }

        //Récupération du score sondage
        private int GetScoreSondageEnBDD()
        {
            SqlConnection connexion = new SqlConnection(SqlConnectionString);
            connexion.Open();

            SqlCommand recuperationScoreSondage =
                new SqlCommand("SELECT Score FROM ReponsePossible", connexion);

            int scoreSondage = (int)recuperationScoreSondage.ExecuteScalar();

            connexion.Close();

            return scoreSondage;
        }

        public ActionResult AutreChose()
        {
            return View();
        }
    }
}