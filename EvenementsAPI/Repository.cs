using EvenementsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenementsAPI
{
    public class Repository
    {
        public static int IdSequenceEvenement { get; set; } = 1;
        public static int IdSequenceCategorie { get; set; } = 1;
        public static ISet<Evenement> Evenements { get; set; } = new HashSet<Evenement>();
        public static ISet<Categorie> Categories { get; set; } = new HashSet<Categorie>();

    }
}
