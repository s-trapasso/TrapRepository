using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDesk.Data.Models
{
    public class VoceIntervento
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Campo obbligatorio"), MaxLength(100)]
        public string Descrizione { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal? Costo { get; set; }

        // Relazione con Manutenzione
        public int ManutenzioneId { get; set; }
        public Manutenzione? Manutenzione { get; set; }
    }
}
