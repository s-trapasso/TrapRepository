using CarManager.App.Models.Owner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManager.App.Components.MappingsUI
{
    public static class OwnerUiMappings
    {
        public static OwnerCreateModel ToCreateModel(this OwnerModel dto)
            => new()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Address = dto.Address,
                BirthDate = dto.BirthDate,
                BirthPlace = dto.BirthPlace,
                Gender = dto.Gender
            };
    }
}
