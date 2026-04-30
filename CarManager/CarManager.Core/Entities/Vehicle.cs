using CarManager.Core.Enums;
using CarManager.Core.Extensions;

namespace CarManager.Core.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Plate { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public FuelTypeEnum FuelType { get; set; }
        public int Km { get; set; }
        // Proprietà calcolata (non mappata se vuoi, ma EF di solito la ignora se solo getter)
        public string BrandModel => $"{Brand} {Model}";

        // --- Tire info ---
        public TireType CurrentTireType { get; set; }
        public string CurrentTireTypeName => CurrentTireType.GetDescription();
        public DateTime? LastTireChangeDate { get; set; }

        // --- Owner relation ---
        public int OwnerId { get; set; }
        public Owner Owner { get; set; } = null!;

        // --- Navigation properties ---
        public ICollection<Maintenance> Maintenance { get; set; } = new List<Maintenance>();

        //public ICollection<Scadenza> Scadenze { get; set; } = new List<Scadenza>();
    }
}
