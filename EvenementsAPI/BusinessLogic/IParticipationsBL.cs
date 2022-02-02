using EvenementsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenementsAPI.BusinessLogic
{
    public interface IParticipationsBL
    {
        public IEnumerable<Participation> GetList();
        public Participation Get(int id);

        public Participation Add(Participation value);
        public Participation Updade(int id, Participation value);

        public Participation Delete(int id);
    }
}
