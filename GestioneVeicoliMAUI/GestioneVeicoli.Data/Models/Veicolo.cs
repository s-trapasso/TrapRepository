using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneVeicoli.Data.Models
{
    public class Veicolo : INotifyPropertyChanged
    {
        private int _id;
        private string _targa;
        private string _marca;
        private string _modello;
        private int _anno;
        private int? km;
        private string alimentazione;
        public event PropertyChangedEventHandler PropertyChanged;

        [Key]
        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }
        [Required]
        [MaxLength(50)]
        public string Targa
        {
            get => _targa;
            set
            {
                if (_targa != value)
                {
                    _targa = value;
                    OnPropertyChanged(nameof(Targa));
                }
            }
        }
        [Required]
        [MaxLength(50)]
        public string Marca
        {
            get => _marca;
            set
            {
                if (_marca != value)
                {
                    _marca = value;
                    OnPropertyChanged(nameof(Marca));
                }
            }
        }
        [Required]
        [MaxLength(50)]
        public string Modello
        {
            get => _modello;
            set
            {
                if (_modello != value)
                {
                    _modello = value;
                    OnPropertyChanged(nameof(Modello));
                }
            }
        }
        [Required]
        [MaxLength(4)]
        public int Anno
        {
            get => _anno;
            set
            {
                if (_anno != value)
                {
                    _anno = value;
                    OnPropertyChanged(nameof(Anno));
                }
            }
        }

        [MaxLength(100)]
        public string Alimentazione
        {
            get => alimentazione;
            set
            {
                if (alimentazione != value)
                {
                    alimentazione = value;
                    OnPropertyChanged(nameof(Alimentazione));
                }
            }
        }
        public int? Km
        {
            get => km;
            set
            {
                if (km != value)
                {
                    km = value;
                    OnPropertyChanged(nameof(Km));
                }
            }
        }
        // Relazione con Manutenzioni
        public ICollection<Manutenzione> Manutenzioni { get; set; }
        public int ProprietarioId { get; set; }  // chiave esterna

        public Proprietario Proprietario { get; set; }  // navigation property

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    

}
