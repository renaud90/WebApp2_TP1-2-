using EvenementsAPI.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenementsAPI.BusinessLogic
{
    public class VillesBL : IVillesBL
    {
        public Ville Add(Ville value)
        {

            ValiderModeleVille(value);

            value.Id = Repository.IdSequenceVille++;
            Repository.Villes.Add(value);

            return value;
        }

        public IEnumerable<Ville> GetList()
        {
            return Repository.Villes;
        }

        public Ville Get(int id)
        {
            var ville = Repository.Villes.FirstOrDefault(x => x.Id == id);
            if (ville == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element introuvable (id = {id})" },
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            return ville;

        }

        public IEnumerable<Evenement> GetEvenements(int id)
        {
            var ville = Repository.Villes.FirstOrDefault(x => x.Id == id);
            if (ville == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element introuvable (id = {id})" },
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            return Repository.Evenements.Where(_ => _.IdVille == id);

        }

        public Ville Updade(int id, Ville value)
        {
            ValiderModeleVille(value);

            var ville = Repository.Villes.FirstOrDefault(x => x.Id == id);


            if (ville == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element introuvable (id = {id})" },
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            ville.Nom = value.Nom;
            ville.Region = value.Region;

            return ville;
        }

        public Ville Delete(int id)
        {
            var ville = Repository.Villes.FirstOrDefault(x => x.Id == id);
            if (ville == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element introuvable (id = {id})" },
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            var villeAssociee = Repository.Evenements.FirstOrDefault(e => e.IdVille == id);

            if (villeAssociee != null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element (id = {id}) ne peut être supprimé, car associé à au moins un événement" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }


            Repository.Villes.Remove(ville);
            return ville;
        }

        private void ValiderModeleVille(Ville value)
        {
            if (value == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = "Parametres d'entrée non valides" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            if (String.IsNullOrEmpty(value.Nom))
            {
                throw new HttpException
                {
                    Errors = new { Errors = "Parametres d'entrée non valides: nom de ville non fourni" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
        }
    }
}
