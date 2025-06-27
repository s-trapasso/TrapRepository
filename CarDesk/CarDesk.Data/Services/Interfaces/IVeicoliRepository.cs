using CarDesk.Data.Models;

namespace CarDesk.Data.Services.Interfaces
{
    public interface IVeicoliRepository
    {
        Task<List<Veicolo>> GetVeicoliAsync();
        Task AddVeicoloAsync(Veicolo veicolo);
        Task UpdateVeicoloAsync(Veicolo veicolo);
        Task DeleteVeicoloAsync(int id);
        Task<Veicolo?> GetVeicoloByIdAsync(int? id);
        Task<Veicolo?> GetVeicoloByTargaAsync(string targa);

    }

}
