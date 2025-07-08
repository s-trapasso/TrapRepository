using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDesk.Data.Models
{
    public class Manutenzione : INotifyPropertyChanged
    {
        private int _id;
        private string _tipoIntervento;
        private DateTime _data;
        private Veicolo? _veicolo;
        private decimal _costo;
        public event PropertyChangedEventHandler? PropertyChanged;

        [Key]
        public int Id
        {
            get => _id;
            set => SetField(ref _id, value);
        }

        [Required, MaxLength(100)]
        public string TipoIntervento 
        { 
            get => _tipoIntervento;
            set => SetField(ref _tipoIntervento,value);
        }

        [Required]
        public DateTime Data
        {
            get => _data;
            set => SetField(ref _data, value);
        }
        
        [Column(TypeName = "decimal(10,2)")]
        public decimal Costo
        {
            get => _costo;
            set => SetField(ref _costo, value);
        }

        [Required]
        public int VeicoloId { get; set; }

        public Veicolo? Veicolo { get; set; }

        

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
