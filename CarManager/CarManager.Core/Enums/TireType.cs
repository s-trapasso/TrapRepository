using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManager.Core.Enums
{
    public enum TireType
    {
        Unknown = 0,

        [Description("Estive")]
        Summer = 1,

        [Description("Invernali")]
        Winter = 2,

        [Description("All Season")]
        AllSeason = 3
    }
}
