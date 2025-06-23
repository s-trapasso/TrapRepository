using System.ComponentModel.DataAnnotations;

namespace CarDesk.Data.Models
{
    public class Manutenzione
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string TipoIntervento { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public int VeicoloId { get; set; }

        public Veicolo Veicolo { get; set; }
    }
}
