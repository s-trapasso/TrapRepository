using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneVeicoli.Data.Models
{
    public class Manutenzione
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string TipoIntervento { get; set; }

        [Required]
        public DateTime Data { get; set; }

        // Chiave esterna per il veicolo
        public int VeicoloId { get; set; }

        // Relazione molti-a-uno con Veicolo
        public Veicolo Veicolo { get; set; }
    }
}
