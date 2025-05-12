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
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    

}
