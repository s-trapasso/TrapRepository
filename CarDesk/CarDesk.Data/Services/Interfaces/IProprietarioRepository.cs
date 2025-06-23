using CarDesk.Data.Models;

namespace CarDesk.Data.Services.Interfaces
{
    public interface IProprietarioRepository
    {
        Task AddProprietarioAsync(Proprietario proprietario);
        Task<List<Proprietario>> GetAllProprietariAsync();
        Task<Proprietario?> GetByIdAsync(int id);
        Task UpdateProprietarioAsync(Proprietario proprietario);
        Task DeleteProprietarioAsync(int id);
        Task<Proprietario> GetProprietarioByDetailsAsync(string nome, string cognome, string indirizzo);
        Task<Proprietario> GetProprietarioByNomeCognomeAsync(string nome, string cognome);
    }

}
