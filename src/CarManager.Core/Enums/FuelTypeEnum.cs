using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManager.Core.Enums
{
    public enum FuelTypeEnum
    {
        Unknown = 0,

        [Description("Benzina")]
        Petrol = 1,

        [Description("Diesel")]
        Diesel = 2,

        [Description("GPL")]
        LPG = 3,

        [Description("Metano")]
        CNG = 4,

        [Description("Elettrica")]
        Electric = 5,

        [Description("Ibrida")]
        Hybrid = 6
    }
}
