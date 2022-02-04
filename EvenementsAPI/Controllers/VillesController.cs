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
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
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
            return Ok(_villesBL.GetList());
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult Post([FromBody] Ville value)
        {
            _villesBL.Add(value);
            return CreatedAtAction(nameof(Get), new { id = value.Id }, null);
        }



        // PUT api/<VillesController>/5
        /// <summary>
        /// Modifier une ville existante
        /// </summary>
        /// <param name="id">ID de la ville</param>
        /// <param name="value">La ville à modifier</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Put(int id, [FromBody] Ville value)
        {
            _villesBL.Updade(id, value);
            return NoContent();
        }

        // DELETE api/<VillesController>/5
        /// <summary>
        /// Suppression d'un événement
        /// </summary>
        /// <param name="id">ID de la ville</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Delete(int id)
        {

            _villesBL.Delete(id);
            return NoContent();
        }

    }
}
