using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManager.Core.Enums
{
    public enum OwnerGender
    {
        Unknown = 0,

        [Description("Maschio")]
        Male = 1,

        [Description("Femmina")]
        Female = 2
    }
}
