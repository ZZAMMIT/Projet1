using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
	public class NouveauSondageModel
	{

		public string Titre { get; set; }

		public string Description { get; set; }

		public int Cle { get; set; }

		//public List<int> NumReponse { get; set; } 
		public int Choix_multi { get; set; }


		public List<string> ListeReponses { get; set; }

		//public NouveauSondageModel(string titre, string description, int cle,List<string> listeReponses)
		//{
		//	this.Titre = titre;
		//	this.Description = description;
		//	this.Cle = cle;
		//	this.ListeReponses = listeReponses;

		//}

	}
	public class ResultatsModel
	{
		public string Titre { get; set; }
		public string Description { get; set; }
		public string ListeReponses { get; set; }
		public int NbVote { get; set; }


		public TuplesReponses Reponses { get; set; }

	}
}