using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EvenementsAPI.Models
{
    public class Ville
    {
        [Required]
        public int? Id { get; set; }
        public string Nom { get; set; }
        public Region Region { get; set; }
    }

    public enum Region
    {
        ESTRIE,
        MAURICIE,
        OUTAOUAI,
        ABITIBI,
        COTE_NORD
    }
}
