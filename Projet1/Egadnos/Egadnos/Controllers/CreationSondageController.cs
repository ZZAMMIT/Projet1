using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Egadnos.Models;
using System.Data.SqlClient;

/*////////////////////////////////////////////////////
 A
///////////////////////////////////////////////////*/

namespace Egadnos.Controllers
{

    /*Insetion éléments dans la BDD table Sondage*/
    public class CreationSondageController : Controller
    {
        private const string SqlConnectionString =
            @"Server=.\SQLExpress;Initial Catalog=GeandosBDD; Trusted_Connection=Yes";

        static int SauvegarderBdd(Questionnaires sondageASauvegarder)
        {
            SqlConnection connexion = new SqlConnection(SqlConnectionString);
            connexion.Open();

            SqlCommand addSondage = new SqlCommand(
                @"INSERT INTO Sondage(Titre, Descriptif, ChoixMultiple) VALUES (@titre, @descriptif, @choixMultiple);
                      SELECT SCOPE_IDENTITY()"
                , connexion);
            addSondage.Parameters.AddWithValue("@titr", sondageASauvegarder.Titre);
            addSondage.Parameters.AddWithValue("@descriptif", sondageASauvegarder.Descriptif);
            addSondage.Parameters.AddWithValue("@choixMultiple", sondageASauvegarder.ChoixMultiple);
            addSondage.ExecuteNonQuery();

            connexion.Close();
        }

        public ActionResult CreaSondage()
        {
            return View("CreSondage");
        }

        New Sondage
        public ActionResult Sondage(string titre, string descriptif, string reponse1, string reponse2, string reponse3, string reponse4)
        {
            Questionnaires = new Questionnaires();
            NewSondage.Titre = titre;
            NewSondage.Descriptif = descriptif;
            NewSondage.VoteUtilisateur = new string[4];

            string[] ReponseUtilisateur = new string[4] { reponse1, reponse2, reponse3, reponse4 };
            foreach (string InTableau in ReponseUtilisateur)
            {
                NewSondage.VoteUtilisateur.Add(InTableau);
            }

            SauvegarderBdd(NewSondage);


            return View();

        }


        static List<string> RecupererDansBDD()

        {
            SqlConnection connexion = new SqlConnection(SqlConnectionString);
            connexion.Open();

            SqlCommand getID = new SqlCommand(

                @"SELECT MAX(IdSondage) FROM Sondage", connexion);
            int dernierID = (int)getID.ExecuteScalar();

            SqlCommand getBDD = new SqlCommand(
            @"SELECT IntituleChoix FROM ChoixPossibles WHERE FkIdSondage=@dernierId", connexion);
            getBDD.Parameters.AddWithValue("@dernierId", dernierID);
            SqlDataReader reader = getBDD.ExecuteReader();
            List<string> choixDansBDD = new List<string>();

            while (reader.Read())
            {
                choixDansBDD.Add((string)reader["IntituleChoix"]);
            }






            //Select from ReponsPpossible
            static ReponsePossible TryRecupererReponsePossibleEnBDD(int reponseId)
        {
            SqlConnection connexion = new SqlConnection(SqlConnectionString);
            connexion.Open();

            SqlCommand commandeRecuperation = new SqlCommand(
                "SELECT NumReponse, Intitule, Score, FKNumSondage FROM Sondage s, ReponsePossible r WHERE s.NumSondage=r.FKNumSondage = @idParam"
                , connexion);
            commandeRecuperation.Parameters.AddWithValue("@idParam", reponseId);

            SqlDataReader reader = commandeRecuperation.ExecuteReader();

            if (reader.Read())
            {
                int numReponseBdd = (int)reader["NumReponse"];
                string intituleBdd = (string)reader["Intitule"];
                string scoreBdd = (string)reader["Score"];
                
                connexion.Close();

                ReponsePossible resultat = new ReponsePossible(
                    numReponseBdd, intituleBdd, scoreBdd);
                return resultat;
            }
            else
            {
                return null;
            }
        }


    }
}