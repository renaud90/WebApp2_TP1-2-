using EvenementsAPI.BusinessLogic;
using EvenementsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenementsAPI.Controllers
{
    public class VillesController : ControllerBase
    {
        private readonly IVillesBL _villesBL;

     public VillesController(IVillesBL villesBL)
        {
            _villesBL = villesBL;
        }

        // GET: api/<VillesController>
        /// <summary>
        /// Lister tous les Villes enregistré
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Ville>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Ville>> Get()
        {
            return _villesBL.GetList().ToList();
        }

        // GET api/<VillesController>/5
        /// <summary>
        /// Obtenir les detail d'une Ville a partir de son id
        /// </summary>
        /// <param name="id">Identifiant de la Ville</param>
        /// <returns><see cref="Ville"/></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Ville), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Ville> Get(int id)
        {
            var ville = _villesBL.Get(id);

            return ville != null ? Ok(ville) : NotFound();
        }

        // POST api/<VillesController>
        /// <summary>
        /// Permet d'ajouter un Ville
        /// </summary>
        /// <param name="value"><see cref="Ville"/> Nouvelle Ville</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Post([FromBody] Ville value)
        {
            if (value == null)
            {
                return BadRequest(new { Error = "Ville doit pas etre null" });
            }
            value.Id = Repository.IdSequenceVille++;
            _villesBL.Add(value);
             return Ok(value);
        }

        

        // PUT api/<VillesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Put(int id, [FromBody] Ville value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            var ville = _villesBL.Get(id);
            if (ville == null)
            {
                return NotFound();
            }
            ville.Nom = value.Nom;
            ville.Region = value.Region;
            
            
            return NoContent();
        }

        // DELETE api/<VillesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Delete(int id)
        {
            var ville = _villesBL.Get(id);
            if (ville != null)
            {
                Repository.Villes.Remove(ville);
            }
            return NoContent();
        }

    }
}
