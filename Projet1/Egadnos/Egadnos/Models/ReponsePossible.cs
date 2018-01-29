using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Egadnos.Models
{
    public class ReponsePossible
    {
        private int numReponseBdd;
        private string intituleBdd;
        private string scoreBdd;

        public ReponsePossible(int numReponseBdd, string intituleBdd, string scoreBdd)
        {
            this.numReponseBdd = numReponseBdd;
            this.intituleBdd = intituleBdd;
            this.scoreBdd = scoreBdd;
        }

        public int NumReponse {get; set;}
        public string Intitule {get; set;}
        public int Score {get; set;}
        public int FKNumSondage {get; set;}
    }
}