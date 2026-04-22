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
        Task<string?> GetFiscalCodePreviewAsync(OwnerCreateModel ownerCreateModel, CancellationToken cancellationToken = default);
        Task<OwnerModel> GetByIdAsync(int ownerId, CancellationToken cancellationToken = default);
        Task<OwnerModel> UpdateOwnerAsync(int ownerId, OwnerCreateModel ownerUpdateModel, CancellationToken cancellationToken = default);
        Task DeleteOwnerAsync(int ownerId, CancellationToken cancellationToken = default);
    }
}
