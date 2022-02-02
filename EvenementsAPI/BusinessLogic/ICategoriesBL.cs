using EvenementsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenementsAPI.BusinessLogic
{
    public interface ICategoriesBL
    {
        public IEnumerable<Categorie> GetList();
        public Categorie Get(int id);

        public Categorie Add(Categorie value);
        public Categorie Update(int id, Categorie value);
        public void Delete(int id);
    }
}
