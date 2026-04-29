using CarManager.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManager.Core.Models
{
    public class Owner
    {

        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Address { get; set; } = string.Empty;

        [Required]
        public DateTime BirthDate { get; set; }

        [MaxLength(100)]
        public string BirthPlace { get; set; } = string.Empty;

        [Required]
        public OwnerGender Gender { get; set; }

        [MaxLength(32)]
        public string FiscalCode { get; set; } = string.Empty;

        // Navigation Properties
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();


        ////---DETTAGLI PATENTE---
        //[Required(ErrorMessage = "Campo obbligatorio"), MaxLength(20)]
        //public string? NumeroPatente { get; set; }
        //[Required(ErrorMessage = "Campo obbligatorio")]
        //public DateTime? DataRilascioPatente { get; set; }
        //[Required(ErrorMessage = "Campo obbligatorio")]
        //public DateTime? DataScadenzaPatente { get; set; }
        //[NotMapped]
        //public DateTime? DataUltimoRinnovo { get; set; } // Solo calcolo temporaneo
        //[MaxLength(10)]
        //public string? CategoriaPatente { get; set; }

        //// Lista delle categorie di patente (non mappata in EF)
        //[NotMapped]
        //public List<CategoriaPatenteEnum> CategoriePatente
        //{
        //    get
        //    {
        //        if (string.IsNullOrWhiteSpace(CategoriaPatente))
        //            return new List<CategoriaPatenteEnum>();

        //        return CategoriaPatente
        //            .Split(',', StringSplitOptions.RemoveEmptyEntries)
        //            .Select(s => System.Enum.Parse<CategoriaPatenteEnum>(s))
        //            .ToList();
        //    }
        //    set
        //    {
        //        CategoriaPatente = (value == null || !value.Any())
        //            ? null
        //            : string.Join(",", value);
        //    }
        //}
    }
}
