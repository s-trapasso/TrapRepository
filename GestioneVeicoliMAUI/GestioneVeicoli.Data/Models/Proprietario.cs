using System.ComponentModel.DataAnnotations;

namespace GestioneVeicoli.Data.Models
{
    public class Proprietario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(100)]
        public string Cognome { get; set; }

        [Required]
        [MaxLength(200)]
        public string Indirizzo { get; set; }

        // Relazione con Veicolo (uno-a-molti)
        public ICollection<Veicolo> Veicoli { get; set; }
    }
}
