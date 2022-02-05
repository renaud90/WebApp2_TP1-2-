using EvenementsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenementsAPI.BusinessLogic
{
    public interface IVillesBL
    {
        public IEnumerable<Ville> GetList();
        public Ville Get(int id);
        public IEnumerable<Evenement> GetEvenements(int id);

        public Ville Add(Ville value);
        public Ville Updade(int id, Ville value);

        public Ville Delete(int id);
    }
}
