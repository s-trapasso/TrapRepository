using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDesk.Data.Models;

namespace CarDesk.Data.DTOs
{
    public class DatiInizialiDto
    {
        public List<Veicolo> Veicoli { get; set; } = new();
        public List<Proprietario> Proprietari { get; set; } = new();
        public List<Manutenzione> Manutenzioni { get; set; } = new();
        // In futuro aggiungi qui altre liste (Assicurazioni, Revisioni, ecc.)
    }

}
