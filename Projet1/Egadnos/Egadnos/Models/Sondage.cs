using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Egadnos.Models
{
    class Sondage
    {
        public int NumSondage {get; set;}
        public string Titre {get; set;}
        public string Descriptif {get; set;}
        public bool ChoixMultiple {get; set;}
        public string LienVote {get; set; }
        public string LienSuppression {get; set;}
        public string LienConsultation {get; set;}
        public bool SondageActif {get; set;}
    }

    
}