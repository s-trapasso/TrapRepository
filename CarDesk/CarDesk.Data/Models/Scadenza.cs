using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDesk.Data.Models
{
    public class Scadenza    
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int VeicoloId { get; set; }

        [ForeignKey(nameof(VeicoloId))]
        public Veicolo Veicolo { get; set; } = null!;

        [Required]
        public TipoScadenza Tipo { get; set; }

        [Required]
        public DateTime DataScadenza { get; set; }

        public string? Descrizione { get; set; }
        public string? Note { get; set; }

        public bool NotificaAttiva { get; set; } = false;

        public int GiorniPreavviso { get; set; } = 7; // Notifica 7 giorni prima
    }

    public enum TipoScadenza
    {
        Assicurazione,
        Bollo,
        Revisione,
        Altro
    }
}

