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
    public class ParticipationsController : ControllerBase
    {
        private readonly IParticipationsBL _participationsBL;

     public ParticipationsController(IParticipationsBL participationsBL)
        {
            _participationsBL = participationsBL;
        }

        // GET: api/<ParticipationsController>
        /// <summary>
        /// Lister tous les participations enregistré
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Participation>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Participation>> Get()
        {
            return _participationsBL.GetList().ToList();
        }

        // GET api/<ParticipationsController>/5
        /// <summary>
        /// Obtenir les detail d'une participation a partir de son id
        /// </summary>
        /// <param name="id">Identifiant de la participation</param>
        /// <returns><see cref="Participation"/></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Participation), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Participation> Get(int id)
        {
            var participation = _participationsBL.Get(id);

            return participation != null ? Ok(participation) : NotFound();
        }

        // POST api/<ParticipationsController>
        /// <summary>
        /// Permet d'ajouter un Participation
        /// </summary>
        /// <param name="value"><see cref="Participation"/> Nouvelle Participation</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public ActionResult Post([FromBody] Participation value)
        {

            _participationsBL.Add(value);

            return new AcceptedResult { Location = Url.Action(nameof(Status), new { id = value.Id }) };
        }

        // GET api/<ParticipationsController>/5/status
        /// <summary>
        /// Verifier le status de traitement de la creation/ajout d'une Participation 
        /// </summary>
        /// <param name="id">identifian Participation</param>
        /// <returns>retourne le status en tant que string</returns>
        [HttpGet("{id}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status303SeeOther)]
        public ActionResult Status(int id)
        {
            if (_participationsBL.GetStatus(id))
            {
                Response.Headers.Add("Location", Url.Action(nameof(Get), new { id = id }));
                return new StatusCodeResult(StatusCodes.Status303SeeOther);
            }
            else
            {
                return Ok(new { status = "Validation en attente" });
            }  
        }



        // DELETE api/<ParticipationsController>/5
        /// <summary>
        /// Suppression d'une participation
        /// </summary>
        /// <param name="id">ID de la participation</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            _participationsBL.Delete(id);

            return NoContent();
        }
    }
}
