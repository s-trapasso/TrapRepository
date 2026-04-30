using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManager.Core.Enums
{
    public enum MaintenanceType
    {
        Unknown = 0,

        [Description("Cambio olio")]
        OilChange = 1,

        [Description("Cambio gomme")]
        TireChange = 2,

        [Description("Freni")]
        Brakes = 3,

        [Description("Revisione")]
        Inspection = 4,

        [Description("Altro")]
        Other = 99
    }
}
