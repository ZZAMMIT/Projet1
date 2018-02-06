using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
	public class Sondage
	{
		public int? Num_Sondage { get; private set; }

		public int Efface { get; private set; }

		public string Titre { get; private set; }
		public string Descriptif { get; private set; }
		public string Lien_suppression { get; set; }
		public string Lien_vote { get; set; }
		public string Lien_consultation { get; set; }

		public int Choix_mult { get; private set; }

		public Sondage(int? id, string titre, string descriptif, int choix_mult, string lien_vote, string lien_suppression, string lien_consultation, int efface)
		{
			this.Num_Sondage = id;
			this.Titre = titre;
			this.Descriptif = descriptif;
			this.Lien_suppression = lien_suppression;
			this.Lien_vote = lien_vote;
			this.Lien_consultation = lien_consultation;
			this.Choix_mult = choix_mult;
			this.Efface = efface;

		}
	}
	public class Reponses
	{
		public int? Num_reponse { get; private set; }
		public string Intitule_reponse { get; private set; }
		public int Score_reponse { get; private set; }
		public int FKNum_sondage { get; set; }
		public Reponses(int? id, string intitule_reponse, int score_reponse, int fKNum_sondage)
		{
			this.Num_reponse = id;
			this.Intitule_reponse = intitule_reponse;
			this.Score_reponse = score_reponse;
			this.FKNum_sondage = fKNum_sondage;
		}
	}

	public class ListeReponses
	{
		public int? Cle_Sondage { get; set; }
		public int Compteur { get; set; }
		public List<string> Reponses { get; set; }
	}

	public class TuplesReponses
	{
		public string Intitule_reponse { get; set; }
		public int Score_Reponse { get; set; }
		public int SomScore { get; set; }
	}

}







/*{
    /// <summary>
    /// Classe permettant de manipuler une opération
    /// </summary>
    public class Operation
    {
        /// <summary>
        /// Clé primaire en BDD
        /// </summary>
        public int? ID { get; private set; }

        /// <summary>
        /// Opérateur de l'opération
        /// </summary>
        public TypeOperateur Operateur { get; private set; }

        /// <summary>
        /// Opérande de droite de l'opération
        /// </summary>
        public double OperandeDroite { get; private set; }

        /// <summary>
        /// Opérande de gauche de l'opération
        /// </summary>
        public double OperandeGauche { get; private set; }

        /// <summary>
        /// Constructeur paramétré
        /// 
        /// Permet de construire une opération à partir de son opérateur, son opérande de droite et de gauche
        /// </summary>
        /// <param name="operateur">opérateur de l'opération</param>
        /// <param name="operandeDroite">opérande de droite de l'opération</param>
        /// <param name="operandeGauche">opérande de gauche de l'opération</param>
        public Operation(int? id, TypeOperateur operateur, double operandeDroite, double operandeGauche)
        {
            this.ID = id;
            this.Operateur = operateur;
            this.OperandeDroite = operandeDroite;
            this.OperandeGauche = operandeGauche;
        }


        /// <summary>
        /// Constructeur paramétré
        /// 
        /// Permet de construire une opération à partir de son opérateur, son opérande de droite et de gauche
        /// </summary>
        /// <param name="operateur">opérateur de l'opération</param>
        /// <param name="operandeDroite">opérande de droite de l'opération</param>
        /// <param name="operandeGauche">opérande de gauche de l'opération</param>
        public Operation(TypeOperateur operateur, double operandeDroite, double operandeGauche) : this(null, operateur, operandeDroite, operandeGauche)
        {
        }


        /// <summary>
        /// Méthode permettant de déterminer si l'opération courante est valide ou non.
        /// Si elle ne l'est pas, la détermination du résultat pourrait crasher.
        /// </summary>
        /// <returns>vrai si l'opération est valide, false sinon</returns>
        public bool IsValide()
        {
            if (this.Operateur == TypeOperateur.Division && this.OperandeDroite == 0.0)
            {
                return false;
            }
            if (this.Operateur == TypeOperateur.Puissance && this.OperandeGauche < 0 && this.OperandeDroite < 1)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Récupération d'une représentation textuelle lisible par l'utilisateur de l'opération
        /// </summary>
        /// <returns>Représentation textuelle</returns>
        public string GetRepresentationTextuelle()
        {
            return string.Format("Résultat de {0} de {1} et {2} = {3}", this.Operateur, this.OperandeGauche, this.OperandeDroite, this.GetResult());
        }

        /// <summary>
        /// Récupération du résultat de l'opération
        /// </summary>
        /// <returns>récupération du résultat</returns>
        public double GetResult()
        {
            switch (this.Operateur)
            {
                case TypeOperateur.Multiplication:
                    return this.OperandeGauche * this.OperandeDroite;
                case TypeOperateur.Addition:
                    return this.OperandeGauche + this.OperandeDroite;
                case TypeOperateur.Soustraction:
                    return this.OperandeGauche - this.OperandeDroite;
                case TypeOperateur.Division:
                    return this.OperandeGauche / this.OperandeDroite;
                case TypeOperateur.Puissance:
                    return Math.Pow(this.OperandeGauche, this.OperandeDroite);
                default:
                    return 0;
            }
        }
    }
}}
*/
