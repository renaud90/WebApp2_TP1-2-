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

            ValiderModeleDeParticipation(value);

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
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

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
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            Repository.Participations.Remove(Participation);
            return Participation;
        }

        private void ValiderModeleDeParticipation(Participation value)
        {
            if (value == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = "Parametres d'entrée non valides" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            if (String.IsNullOrEmpty(value.Nom) ||
                String.IsNullOrEmpty(value.Prenom) ||
                String.IsNullOrEmpty(value.Courriel) ||
                value.IdEvenement < 1 || value.NbPlaces >= 1)
            {
                throw new HttpException
                {
                    Errors = new { Errors = "Parametres d'entrée non valides" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
           
            if (Repository.Evenements.FirstOrDefault(e => e.Id == value.IdEvenement) == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = "Parametres d'entrée non valides" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            if (Repository.Participations.FirstOrDefault(p => p.Courriel == value.Courriel) != null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = "Parametres d'entrée non valides" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
        }
    }
}
