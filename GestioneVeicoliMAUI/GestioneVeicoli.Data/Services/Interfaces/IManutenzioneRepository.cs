using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestioneVeicoli.Data.Models;

namespace GestioneVeicoli.Data.Services.Interfaces
{
    public interface IManutenzioneRepository
    {
        Task<List<Manutenzione>> GetByVeicoloIdAsync(int veicoloId);
        Task AddAsync(Manutenzione manutenzione);
        Task UpdateAsync(Manutenzione manutenzione);
        Task DeleteAsync(int id);
        Task<List<Manutenzione>> GetAllManutenzioniAsync();
    }
}
