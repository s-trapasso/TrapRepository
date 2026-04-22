using CarManager.Core.Enum;
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
        public string Plate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public FuelTypeEnum FuelType { get; set; }
        public int Km { get; set; }
        public string BrandModel => $"{Brand} {Model}";

        //// Navigation Properties
        public ICollection<Maintenance> Maintenance { get; set; } = new List<Maintenance>();

        public TireType CurrentTireType { get; set; }
        public DateTime? LastTireChangeDate { get; set; }

        //[Required(ErrorMessage ="Campo obbligatorio")]
        //public int ProprietarioId { get; set; }

        //public Proprietario Proprietario { get; set; }

        //public ICollection<Scadenza> Scadenze { get; set; } = new List<Scadenza>();
    }
}
