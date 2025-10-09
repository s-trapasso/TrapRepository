using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using CarDesk.Data.Models.Enum;

namespace CarDesk.Data.Models
{
    public class VoceIntervento
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio"), MaxLength(100)]
        public string Descrizione { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal? Costo { get; set; }

        // Proprietà non mappata, serve solo per MudSelect e logica
        [NotMapped]
        public TipoInterventoEnum? TipoIntervento
        {
            get => _tipoIntervento;
            set
            {
                _tipoIntervento = value;
                // Aggiorna Descrizione solo se non è "Altra Voce"
                if (value.HasValue && value != TipoInterventoEnum.AltraVoce)
                    Descrizione = value.Value.GetDisplayName();
            }
        }

        private TipoInterventoEnum? _tipoIntervento;

        // Relazione con Manutenzione
        public int ManutenzioneId { get; set; }
        public Manutenzione? Manutenzione { get; set; }

        // Proprietà calcolata (non mappata)
        [NotMapped]
        public string DescrizioneFinale
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Descrizione))
                    return Descrizione;

                if (TipoIntervento.HasValue)
                    return TipoIntervento.Value.GetDisplayName();

                return string.Empty;
            }
        }
        public void InitializeTipoInterventoFromDescrizione()
        {
            if (string.IsNullOrWhiteSpace(Descrizione))
            {
                _tipoIntervento = null;
                return;
            }

            foreach (TipoInterventoEnum tipo in System.Enum.GetValues(typeof(TipoInterventoEnum)))
            {
                if (tipo.GetDisplayName() == Descrizione)
                {
                    _tipoIntervento = tipo;
                    return;
                }
            }

            // Se la descrizione non corrisponde a nessun enum → Altra voce
            _tipoIntervento = TipoInterventoEnum.AltraVoce;
        }

    }

    // Extension per leggere DisplayName dall'enum
    public static class EnumExtensions
    {
        public static string GetDisplayName(this System.Enum enumValue)
        {
            var displayAttribute = enumValue.GetType()
                .GetField(enumValue.ToString())
                .GetCustomAttribute<DisplayAttribute>();

            return displayAttribute?.Name ?? enumValue.ToString();
        }
    }

}
