using CarManager.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManager.App.Models.Vehicle
{
    public class VehicleModel
    {
        public int Id { get; set; }
        public string Plate { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public FuelTypeEnum FuelType { get; set; }
        public string FuelTypeName { get; set; } = string.Empty;
        public int Km { get; set; }
       
    }
}
