using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManager.Core.Enums
{
    public enum ErrorCode
    {
        None = 0,

        // GENERICI
        Unknown,
        ValidationError,

        // ENTITY
        NotFound,

        // VEHICLE
        DuplicatePlate,

        // MAINTENANCE
        VehicleNotFound,

        // OWNER
        OwnerNotFound,

        //OWNER FISCAL CODE DUPLICATE
        DuplicateFiscalCode,

        //INVALID FISCAL CODE
        InvalidFiscalCode
    }
}
