using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GestioneVeicoli.Data.Models
{
    public class Proprietario : INotifyPropertyChanged
    {
        // Proprietà per la gestione del proprietario
        // Id del proprietario
        private int _id;
        // Nome del proprietario
        private string _nome;
        // Cognome del proprietario
        private string _cognome;
        // Indirizzo del proprietario
        private string _indirizzo;

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
        [MaxLength(100)]
        public string Nome
        {
            get => _nome;
            set
            {
                if (_nome != value)
                {
                    _nome = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        [Required]
        [MaxLength(100)]
        public string Cognome
        {
            get => _cognome;
            set
            {
                if (_cognome != value)
                {
                    _cognome = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        [Required]
        [MaxLength(200)]
        public string Indirizzo
        {
            get => _indirizzo;
            set
            {
                if (_indirizzo != value)
                {
                    _indirizzo = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        // Relazione con Veicolo (uno-a-molti)
        public ICollection<Veicolo> Veicoli { get; set; }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
