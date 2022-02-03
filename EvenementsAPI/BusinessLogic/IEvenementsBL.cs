using EvenementsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenementsAPI.BusinessLogic
{
    public interface IEvenementsBL
    {
        public IEnumerable<Evenement> GetList();
        public Evenement Get(int id);
        public IEnumerable<Participation> GetParticipations(int id);
        public Evenement Add(Evenement value);
        public Evenement Update(int id, Evenement value);
        public void Delete(int id);
    }
}
