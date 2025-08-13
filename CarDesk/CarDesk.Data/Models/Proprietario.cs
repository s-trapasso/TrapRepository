using CarDesk.Data.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public DateTime? DataNascita { get; set; }

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
        public DateTime? DataRilascioPatente { get; set; }
        [Required(ErrorMessage ="Campo obbligatorio")]
        public DateTime? DataScadenzaPatente { get; set; }
        [MaxLength(10)]
        public string? CategoriaPatente { get; set; }

        // Lista delle categorie di patente (non mappata in EF)
        [NotMapped]
        public List<CategoriaPatenteEnum> CategoriePatente
        {
            get
            {
                if (string.IsNullOrWhiteSpace(CategoriaPatente))
                    return new List<CategoriaPatenteEnum>();

                return CategoriaPatente
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => System.Enum.Parse<CategoriaPatenteEnum>(s))
                    .ToList();
            }
            set
            {
                CategoriaPatente = (value == null || !value.Any())
                    ? null
                    : string.Join(",", value);
            }
        }
    }
}
