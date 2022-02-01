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
        public static ISet<Evenement> Villes { get; set; } = new HashSet<Evenement>();

    }
}
