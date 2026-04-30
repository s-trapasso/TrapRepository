using CarManager.Core.Enums;
namespace CarManager.Core.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; } = string.Empty;
        public OwnerGender Gender { get; set; }
        public string FiscalCode { get; set; } = string.Empty;
        // Navigation Properties
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

        ////---DETTAGLI PATENTE---
        //public string? NumeroPatente { get; set; }       
        //public DateTime? DataRilascioPatente { get; set; }    
        //public DateTime? DataScadenzaPatente { get; set; }
        //public DateTime? DataUltimoRinnovo { get; set; } 
        //public string? CategoriaPatente { get; set; }
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
