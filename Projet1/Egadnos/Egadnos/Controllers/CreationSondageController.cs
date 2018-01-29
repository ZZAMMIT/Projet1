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

    /*Insetion éléments dans la BDD table ReponsePossible*/
    public class CreationSondageController : Controller
    {
        private const string SqlConnectionString =
            @"Server=.\SQLExpress;Initial Catalog=GeandosBDD; Trusted_Connection=Yes";
        static int SauvegarderBdd(Sondage sondageASauvegarder)
        {
            SqlConnection connexion = new SqlConnection(SqlConnectionString);
            connexion.Open();

            SqlCommand command = new SqlCommand(
                @"INSERT INTO Sondage(Titre, Descriptif, ChoixMultiple) VALUES (@titre, @descriptif, @choixMultiple);
                      SELECT SCOPE_IDENTITY()"
                , connexion);
            command.Parameters.AddWithValue("@titr", sondageASauvegarder.Titre);
            command.Parameters.AddWithValue("@descriptif", sondageASauvegarder.Descriptif);
            command.Parameters.AddWithValue("@choixMultiple", sondageASauvegarder.ChoixMultiple);
            int idInsere = Convert.ToInt32(command.ExecuteScalar());
            connexion.Close();
            return idInsere;
        }


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