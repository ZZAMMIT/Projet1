using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Data.SqlClient;


namespace WebApplication1.Controllers
{

	public class AccueilController : Controller
	{
		;
		public ActionResult Index()/*page d'accueil*/
		{
			return View();
		}

		public ActionResult VersCreationSondage()/*vue de création sondage*/
		{
			return View("CreationSondage");
		}

		private const string SqlConnectionString =
			@"Server=./;Initial Catalog=Projet1;Trusted_Connection=Yes";
		// @"Server=172.19.240.123;Database=ADAI-MZ_DR;user Id=sa;password=pf68*CCI";//


		public ActionResult CreerNouveauSondage(string Titre, string Description, int Choix, List<string> Reponse)
		{


			SqlConnection connexion = new SqlConnection(SqlConnectionString);
			connexion.Open();

			SqlCommand command = new SqlCommand(
				@"INSERT INTO Sondage(Titre_Sondage, Description, Choix_multi,Lien_Vote,Lien_Suppression,Lien_Resultat,Sondage_supprime) VALUES (@titre, @desc, @choix,@lienv,@liens,@lienr,@suppr);
				SELECT SCOPE_IDENTITY()", connexion);
			command.Parameters.AddWithValue("@titre", Titre);
			command.Parameters.AddWithValue("@desc", Description);
			command.Parameters.AddWithValue("@choix", Choix);
			command.Parameters.AddWithValue("@lienv", "");
			command.Parameters.AddWithValue("@liens", "");
			command.Parameters.AddWithValue("@lienr", "");
			command.Parameters.AddWithValue("@suppr", 0);
			var cle = command.ExecuteScalar();

			foreach (string insererReponse in Reponse)
			{
				if (!string.IsNullOrEmpty(insererReponse))
				{
					SqlCommand command2 = new SqlCommand(
					@"INSERT INTO Reponses(Intitule,FK_Num_Sondage,score) VALUES (@Reponse,@Clesecondaire,@score)", connexion);

					command2.Parameters.AddWithValue("@Reponse", insererReponse);
					command2.Parameters.AddWithValue("@Clesecondaire", cle);/*injection de la clé primaire du sondage dans la table réponse*/
					command2.Parameters.AddWithValue("@score", 0);/*initialisation des scores des votes pour chaque réponse*/
					command2.ExecuteScalar();
				}
			}
			
			connexion.Close();

			return ChoixMultiple(69);/*en théorie on devrait passer à la vue des liens...*/
		}

		private ActionResult ChoixMultiple(int cle)/*L'action ChoixMultiple traite en fait tous les cas*/
		{
			NouveauSondageModel formulaire = new NouveauSondageModel(); /*///on instantie un nouveau modèle à passer dans un formulaire*/
			SqlConnection connexion = new SqlConnection(SqlConnectionString);
			connexion.Open();

			SqlCommand commandeRecuperation = new SqlCommand("SELECT Titre_Sondage,Description,Choix_multi FROM Sondage WHERE Num_Sondage=@idParam;", connexion);
			commandeRecuperation.Parameters.AddWithValue("@idParam", cle);


			SqlDataReader reader = commandeRecuperation.ExecuteReader();

			if (reader.Read())
			{
				string Titre = (string)reader["Titre_Sondage"];/*///récupération des champs du sondage */
				formulaire.Titre = Titre;

				string Desc = (string)reader["Description"];
				formulaire.Description = Desc;
				int Choix = (int)reader["Choix_multi"]; ;
				formulaire.Choix_multi = Choix;

			}
			formulaire.Cle = cle;
			reader.Close();
			SqlCommand commandeRecuperation2 = new SqlCommand("SELECT Intitule FROM Reponses WHERE FK_Num_Sondage=@idParam;", connexion);
			commandeRecuperation2.Parameters.AddWithValue("@idParam", cle);

			formulaire.ListeReponses = new List<string>();

			SqlDataReader reader2 = commandeRecuperation2.ExecuteReader();
			while (reader2.Read())
			{
				string Intitule = (string)reader2["Intitule"];/*///récupération des réponses correspondantes dans une liste*/
				formulaire.ListeReponses.Add(Intitule);

			}
			connexion.Close();
			if (formulaire.Choix_multi == 2)
			{
				return View("ChoixMultiple", formulaire);/*Dispatching en fonction des choix uniques ou multiples*/
			}
			return View("ChoixUnique", formulaire);
		}








		public ActionResult ReceptionVote(int prodId, List<string> reponse)/*///retour des formulaires de vote avec la clé primaire sondage et la ou les réponses choisies///*/
		{

			ListeReponses Votes = new ListeReponses();
			Votes.Cle_Sondage = prodId;


			SqlConnection connexion = new SqlConnection(SqlConnectionString);
			connexion.Open();


			foreach (string choixReponse in reponse)
			{
				SqlCommand commandeRecuperation = new SqlCommand("UPDATE Reponses Set Reponses.score+=1 WHERE FK_Num_Sondage=@idParam and Intitule=@rep;", connexion);/*///on augmente le score///*/
				commandeRecuperation.Parameters.AddWithValue("@idParam", Votes.Cle_Sondage);/*/// de chaque réponse choisie///*/
				commandeRecuperation.Parameters.AddWithValue("@rep", choixReponse);
				commandeRecuperation.ExecuteNonQuery();
			}

			/////bon il s'agit de calculer à présent et de présenter les résultats///
			/////Création d'un ResultatModel qu'on envoie à la vue adhoc///

			ResultatsModel ResultatdesVotesDeceSondage = new ResultatsModel();
			SqlCommand commandeRecuperation2 = new SqlCommand("SELECT Titre_Sondage,Description FROM Sondage WHERE Num_Sondage=@idParam;", connexion);
			commandeRecuperation2.Parameters.AddWithValue("@idParam", Votes.Cle_Sondage);
			ResultatdesVotesDeceSondage.Titre = "Titre_Sondage";
			ResultatdesVotesDeceSondage.Description = "Description";
			SqlDataReader reader = commandeRecuperation2.ExecuteReader();
			reader.Close();
			SqlCommand commandeRecuperation3 = new SqlCommand("SELECT Intitule,score FROM Reponses WHERE FK_Num_Sondage=@idparam and score<>0 ORDER BY score DESC;", connexion);
			commandeRecuperation3.Parameters.AddWithValue("@idParam", Votes.Cle_Sondage);

			List<TuplesReponses> resultats = new List<TuplesReponses>();			/*Cela reste à peaufiner....dommage*/
			TuplesReponses CoupleDonnees = new TuplesReponses();
			SqlDataReader reader2 = commandeRecuperation3.ExecuteReader();
			while (reader2.Read())
			{
				string ReponseBdd = (string)reader["Intitule"];
				int ScoreBdd = (int)reader["score"];

				CoupleDonnees.Intitule_reponse = ReponseBdd;
				CoupleDonnees.Score_Reponse = ScoreBdd;
			}

			resultats.Add(CoupleDonnees);
			connexion.Close();


			return View("Resultats", resultats);
		}
	}

}


