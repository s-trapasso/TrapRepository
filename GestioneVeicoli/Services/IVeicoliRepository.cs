using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestioneVeicoli.Models;

namespace GestioneVeicoli.Services
{
    public interface IVeicoliRepository
    {
        Task<List<Veicolo>> GetVeicoliAsync();
        Task AddVeicoloAsync(Veicolo veicolo);
        Task UpdateVeicoloAsync(Veicolo veicolo);
        Task DeleteVeicoloAsync(int id);
        //Task<Veicolo> GetVeicoloByIdAsync(int? id);

    }

}
