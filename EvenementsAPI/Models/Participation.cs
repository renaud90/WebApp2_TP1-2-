using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EvenementsAPI.Models
{
    public class Participation
    {
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Courriel invalid")]
        public string Courriel { get; set; }
        [Required]
        public int NbPlaces { get; set; }
        public bool IsValid { get; set; } = false;
        [Required]
        public int IdEvenement { get; set; }

    }
}
