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

            if (value == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = "Parametres d'entrés non valides" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

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
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            return ville;

        }

        public Ville Updade(int id, Ville value)
        {
            if (value == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = "Parametres d'entrés non valides" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            var ville = Repository.Villes.FirstOrDefault(x => x.Id == id);


            if (ville == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element introuvable (id = {id})" },
                    StatusCode = StatusCodes.Status400BadRequest
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
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            Repository.Villes.Remove(ville);
            return ville;
        }
    }
}
