using EvenementsAPI.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenementsAPI.BusinessLogic
{
    public class ParticipationsBL : IParticipationsBL
    {
        public Participation Add(Participation value)
        {

            if (value == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = "Parametres d'entrés non valides" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            value.Id = Repository.IdSequenceParticipation++;
            Repository.Participations.Add(value);

            return value;
        }

        public IEnumerable<Participation> GetList()
        {
            return Repository.Participations;
        }

        public Participation Get(int id)
        {
            var Participation = Repository.Participations.FirstOrDefault(x => x.Id == id);
            if (Participation == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element introuvable (id = {id})" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            return Participation;

        }

        public Participation Updade(int id, Participation value)
        {
            if (value == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = "Parametres d'entrés non valides" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            var Participation = Repository.Participations.FirstOrDefault(x => x.Id == id);


            if (Participation == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element introuvable (id = {id})" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            Participation.Nom = value.Nom;
            Participation.Prenom = value.Prenom;
            Participation.Courriel = value.Courriel;
            Participation.NbPlaces = value.NbPlaces;

            return Participation;
        }

        public Participation Delete(int id)
        {
            var Participation = Repository.Participations.FirstOrDefault(x => x.Id == id);
            if (Participation == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element introuvable (id = {id})" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            Repository.Participations.Remove(Participation);
            return Participation;
        }
    }
}
