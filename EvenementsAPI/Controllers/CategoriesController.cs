using EvenementsAPI.BusinessLogic;
using EvenementsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace EvenementsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesBL _categoriesBL;

        public CategoriesController(ICategoriesBL categoriesBL)
        {
            _categoriesBL = categoriesBL;
        }

        // GET: api/<UsagersController>
        /// <summary>
        /// Lister toutes les catégories d'événement de l'application
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<Categorie>), (int)HttpStatusCode.OK)]
        public ActionResult<IEnumerable<Categorie>> Get()
        {
            return Ok(_categoriesBL.GetList());
        }

        // GET api/usagers/5
        /// <summary>
        /// Obtenir une catégorie par son ID
        /// </summary>
        /// <param name="id"> ID de la categorie</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Categorie), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult<Categorie> Get(int id)
        {
            var categorie = _categoriesBL.Get(id);
            return categorie is null ? NotFound(new { Errors = $"Element introuvable (id = {id})" }) : Ok(categorie);
        }

        // POST api/usagers
        /// <summary>
        /// Ajouter une nouvelle catégorie
        /// </summary>
        /// <param name="value">La catégorie à ajouter</param>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [Consumes(MediaTypeNames.Application.Json)]
        public ActionResult Post([FromBody] Categorie value)
        {
            value = _categoriesBL.Add(value);
            return CreatedAtAction(nameof(Get), new { id = value.Id }, null);

        }

        // PUT api/usagers/5
        /// <summary>
        /// Modifier une catégorie existante
        /// </summary>
        /// <param name="id"> ID de la categorie</param>
        /// <param name="value">La catégorie à modifier</param>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult Put(int id, [FromBody] Categorie value)
        {
            _categoriesBL.Update(id, value);
            return NoContent();
        }

        // DELETE api/usagers/5
        /// <summary>
        /// Suppression d'une catégorie
        /// </summary>
        /// <param name="id">ID de la catégorie</param>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult Delete(int id)
        {
            _categoriesBL.Delete(id);
            return NoContent();
        }
    }
}
