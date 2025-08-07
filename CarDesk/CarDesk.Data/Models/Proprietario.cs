using System.ComponentModel.DataAnnotations;

namespace CarDesk.Data.Models
{
    public class Proprietario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Campo obbligatorio"), MaxLength(100)]
        public string Nome { get; set; }


        [Required(ErrorMessage ="Campo obbligatorio"), MaxLength(100)]
        public string Cognome { get; set; }

        [Required(ErrorMessage ="Campo obbligatorio"), MaxLength(100)]
        public string Indirizzo { get; set; }
        [Required(ErrorMessage ="Campo obbligatorio")]
        public DateTime DataNascita { get; set; }

        [Required(ErrorMessage ="Campo obbligatorio"), MaxLength(100)]
        public string LuogoNascita { get; set; }

        [Required(ErrorMessage ="Campo obbligatorio"), MaxLength(1)]
        public string Sesso { get; set; } // "M" o "F"

        [Required(ErrorMessage ="Campo obbligatorio"), MaxLength(16)]
        public string CodiceFiscale { get; set; }
        public ICollection<Veicolo> Veicoli { get; set; } = new List<Veicolo>();


        //---DETTAGLI PATENTE---
        [Required(ErrorMessage ="Campo obbligatorio"), MaxLength(20)]
        public string? NumeroPatente { get; set; }
        [Required(ErrorMessage ="Campo obbligatorio")]
        public DateTime DataRilascioPatente { get; set; }
        [Required(ErrorMessage ="Campo obbligatorio")]
        public DateTime DataScadenzaPatente { get; set; }
        [MaxLength(10)]
        public string? CategoriaPatente { get; set; }
    }
}
