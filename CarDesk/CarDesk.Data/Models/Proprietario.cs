using System.ComponentModel.DataAnnotations;

namespace CarDesk.Data.Models
{
    public class Proprietario
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Nome { get; set; }


        [Required, MaxLength(100)]
        public string Cognome { get; set; }

        [Required, MaxLength(100)]
        public string Indirizzo { get; set; }

        public ICollection<Veicolo> Veicoli { get; set; } = new List<Veicolo>();
    }
}
