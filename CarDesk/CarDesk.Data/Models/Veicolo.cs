using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarDesk.Data.Models
{
    public class Veicolo
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Targa { get; set; }

        [Required, MaxLength(50)]
        public string Marca { get; set; }

        [Required, MaxLength(50)]
        public string Modello { get; set; }

        [Required]
        public int Anno { get; set; }

        public AlimentazioneEnum? Alimentazione { get; set; }

        public int? Km { get; set; }
        public string MarcaModello => $"{Marca} {Modello}";

        // Navigation Properties
        public ICollection<Manutenzione> Manutenzioni { get; set; } = new List<Manutenzione>();

        [Required]
        public int ProprietarioId { get; set; }

        public Proprietario Proprietario { get; set; }

        public ICollection<Scadenza> Scadenze { get; set; } = new List<Scadenza>();
    }
}
