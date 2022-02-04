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
            if (value == null)
            {
                return BadRequest(new { Error = "Participation doit pas etre null" });
            }

            value.Id = Repository.IdSequenceParticipation++;
            value.IsValid = false;
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
            var Participation = _participationsBL.Get(id);
            if (Participation == null) 
            {
                return NotFound();
            }

            if (Participation.IsValid) {
                Response.Headers.Add("Location", Url.Action(nameof(Get), new { id = id }));
                return new StatusCodeResult(StatusCodes.Status303SeeOther);
            }
            verifyParticipation(Participation);

            return Ok(new { status = "Validation en attente" });

        }

        

        // DELETE api/<ParticipationsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Delete(int id)
        {
            var participation = _participationsBL.Get(id);
            if (participation != null)
            {
                Repository.Participations.Remove(participation);
            }
            return NoContent();
        }


        private void verifyParticipation(Participation Participation)
        {
            var isValid = new Random().Next(1, 10) > 5 ? true : false;//Simuler la validation externe;
            Participation.IsValid = isValid;
        }






    }
}
