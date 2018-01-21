using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Egadnos.Models
{
    public class ReponsePossible
    {
        public int NumReponse {get; set;}
        public string Intitule {get; set;}
        public int Score {get; set;}
        public int FKNumSondage {get; set;}
    }
}