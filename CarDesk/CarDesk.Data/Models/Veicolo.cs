using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarDesk.Data.Models
{
    public class Veicolo : INotifyPropertyChanged
    {
        private int _id;
        private string _targa;
        private string _marca;
        private string _modello;
        private int _anno;
        private int? _km;
        private AlimentazioneEnum? _alimentazione;

        public event PropertyChangedEventHandler? PropertyChanged;

        [Key]
        public int Id
        {
            get => _id;
            set => SetField(ref _id, value);
        }

        [Required, MaxLength(50)]
        public string Targa
        {
            get => _targa;
            set => SetField(ref _targa, value);
        }

        [Required, MaxLength(50)]
        public string Marca
        {
            get => _marca;
            set => SetField(ref _marca, value);
        }

        [Required, MaxLength(50)]
        public string Modello
        {
            get => _modello;
            set => SetField(ref _modello, value);
        }

        [Required]
        public int Anno
        {
            get => _anno;
            set => SetField(ref _anno, value);
        }

        public AlimentazioneEnum? Alimentazione
        {
            get => _alimentazione;
            set => SetField(ref _alimentazione, value);
        }

        public int? Km
        {
            get => _km;
            set => SetField(ref _km, value);
        }

        public string MarcaModello => $"{Marca} {Modello}";

        // Navigation Properties
        public ICollection<Manutenzione> Manutenzioni { get; set; } = new List<Manutenzione>();

        [Required]
        public int ProprietarioId { get; set; }

        public Proprietario Proprietario { get; set; }

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetField<T>(ref T field, T value, string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName ?? string.Empty);
            return true;
        }
    }
}
