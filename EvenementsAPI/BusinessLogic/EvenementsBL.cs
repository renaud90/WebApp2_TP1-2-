﻿using EvenementsAPI.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenementsAPI.BusinessLogic
{
    public class EvenementsBL : IEvenementsBL
    {
        public IEnumerable<Evenement> GetList() 
        {
            return Repository.Evenements;
        }
        public Evenement Get(int id) 
        {
            return Repository.Evenements.FirstOrDefault(_ => _.Id == id);
        }
        public IEnumerable<Participation> GetParticipations(int id) 
        {
            return Repository.Participations.Where(_ => _.IdEvenement == id);
        }
        public Evenement Add(Evenement value) 
        {
            ValiderModele(value);

            value.Id = Repository.IdSequenceEvenement++;
            Repository.Evenements.Add(value);

            return value;
        }
        public Evenement Update(int id, Evenement value) 
        {
            ValiderModele(value);

            var evenement = Repository.Evenements.FirstOrDefault(x => x.Id == id);

            if (evenement == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element introuvable (id = {id})" },
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            evenement.Adresse = value.Adresse;
            evenement.DateDebut = value.DateDebut;
            evenement.DateFin = value.DateFin;
            evenement.Description = value.Description;
            evenement.IdsCategories = value.IdsCategories;
            evenement.IdVille = value.IdVille;
            evenement.NomOrganisateur = value.NomOrganisateur;
            evenement.Prix = value.Prix;
            evenement.Titre = value.Titre;
            
            return evenement;
        }
        public void Delete(int id) 
        {
            var evenement = Repository.Evenements.FirstOrDefault(x => x.Id == id);

            if (evenement == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element inexistant (id = {id})" },
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            Repository.Evenements.Remove(evenement);
            while(Repository.Participations.FirstOrDefault(_ => _.IdEvenement == id) != null)
            {
                var participation = Repository.Participations.FirstOrDefault(_ => _.IdEvenement == id);
                Repository.Participations.Remove(participation);
            }
            
        }

        private void ValiderModele(Evenement value)
        {
            if (value == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = "Parametres d'entrée non valides" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            //if (String.IsNullOrEmpty(value.Nom))
            //{
            //    throw new HttpException
            //    {
            //        Errors = new { Errors = "Parametres d'entrée non valides: nom de catégorie inexistant" },
            //        StatusCode = StatusCodes.Status400BadRequest
            //    };
            //}
        }
    }
}