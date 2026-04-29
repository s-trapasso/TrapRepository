using CarManager.Core.Enums;
using CarManager.Core.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManager.Core.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string Plate { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Brand { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Model { get; set; } = string.Empty;

        [Required]
        public int Year { get; set; }

        [Required]
        public FuelTypeEnum FuelType { get; set; }

        [Required]
        public int Km { get; set; }

        // Proprietà calcolata (non mappata se vuoi, ma EF di solito la ignora se solo getter)
        public string BrandModel => $"{Brand} {Model}";

        // --- Tire info ---
        [Required]
        public TireType CurrentTireType { get; set; }

        public string CurrentTireTypeName => CurrentTireType.GetDescription();

        public DateTime? LastTireChangeDate { get; set; }

        // --- Owner relation ---
        [Required]
        public int OwnerId { get; set; }

        public Owner Owner { get; set; } = null!;

        // --- Navigation properties ---
        public ICollection<Maintenance> Maintenance { get; set; } = new List<Maintenance>();

        //public ICollection<Scadenza> Scadenze { get; set; } = new List<Scadenza>();
    }
}
