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
            value.IsValid = false;
            Repository.Participations.Add(value);

            return value;
        }

        public bool GetStatus(int id)
        {
            var participation = Repository.Participations.FirstOrDefault(_ => _.Id == id);
            if (participation == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element introuvable (id = {id})" },
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            if (participation.IsValid)
            {
                return true;
            }
            verifyParticipation(participation);
            return false;
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
                value.IdEvenement < 1 || value.NbPlaces < 1)
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
                    Errors = new { Errors = $"Parametres d'entrée non valides: événement avec Id {value.IdEvenement} inexistant" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            if (Repository.Participations.FirstOrDefault(p => p.Courriel == value.Courriel && p.IdEvenement == value.IdEvenement) != null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Parametres d'entrée non valides: une participation enregistrée à l'adresse courriel {value.Courriel} pour l'événement avec Id {value.IdEvenement} existe déjà" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
        }

        private void verifyParticipation(Participation Participation)
        {
            var isValid = new Random().Next(1, 10) > 5 ? true : false;//Simuler la validation externe;
            Participation.IsValid = isValid;
        }
    }
}
