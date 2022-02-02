using EvenementsAPI.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenementsAPI.BusinessLogic
{
    public class CategoriesBL:ICategoriesBL
    {
        public IEnumerable<Categorie> GetList()
        {
            return Repository.Categories;
        }
        public Categorie Get(int id)
        {
            return Repository.Categories.FirstOrDefault(x => x.Id == id);
        }

        public Categorie Add(Categorie value)
        {
            if(value == null)
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
                    Errors = new { Errors = "Parametres d'entrée non valides: nom de catégorie inexistant" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            value.Id = Repository.IdSequenceCategorie++;
            Repository.Categories.Add(value);

            return value;
        }
        public Categorie Update(int id, Categorie value)
        {
            if (value == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = "Parametres d'entrée non valides" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            var categorie = Repository.Categories.FirstOrDefault(x => x.Id == id);

            if (categorie == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element introuvable (id = {id})" },
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            categorie.Nom = value.Nom;

            return categorie;
        }
        public void Delete(int id)
        {
            var categorie = Repository.Categories.FirstOrDefault(x => x.Id == id);

            if (categorie == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element inexistant (id = {id})" },
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            var categorieAssociee = Repository.Evenements.Where(_ => _.IdsCategories.Contains(id)).Count() > 0;

            if (categorieAssociee)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element (id = {id}) ne peut être supprimé, car associé à au moins un événement" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            Repository.Categories.Remove(categorie);
        }
    }
}
