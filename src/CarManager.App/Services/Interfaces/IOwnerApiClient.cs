using CarManager.App.Models.Owner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManager.App.Services.Interfaces
{
    public interface IOwnerApiClient
    {
        Task<IReadOnlyList<OwnerModel>> GetAllOwnersAsync(CancellationToken cancellationToken = default);
        Task<OwnerModel> CreateOwnerAsync(OwnerCreateModel ownerCreateModel, CancellationToken cancellationToken = default);
    }
}
