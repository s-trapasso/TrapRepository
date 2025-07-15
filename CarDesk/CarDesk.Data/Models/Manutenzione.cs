using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDesk.Data.Models
{
    public class Manutenzione 
    {
       

        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Costo { get; set; }

        [Required]
        public int VeicoloId { get; set; }

        public Veicolo? Veicolo { get; set; }

        // Navigation Properties
        public ICollection<VoceIntervento> VociIntervento { get; set; } = new List<VoceIntervento>();

        public decimal CostoTotale => VociIntervento?.Sum(v => v.Costo) ?? 0;
    }
}
