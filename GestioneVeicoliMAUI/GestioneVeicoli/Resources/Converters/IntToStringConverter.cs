using System.Globalization;

namespace GestioneVeicoli.Resources.Converters
{
    public class IntToStringConverter : IValueConverter
    {
        public int DefaultValue { get; set; } = 0; // Imposta qui il valore di default che vuoi trattare come "vuoto"

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue && intValue == DefaultValue)
            {
                return string.Empty; // Se il valore è uguale al valore di default, ritorna una stringa vuota
            }
            return value?.ToString() ?? string.Empty; // Altrimenti, converte l'int in stringa
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value as string))
            {
                return DefaultValue; // Se il valore è vuoto o nullo, ritorna il valore predefinito
            }
            return int.TryParse(value as string, out int result) ? result : DefaultValue; // Se il valore non è un int valido, ritorna il valore predefinito
        }
    }
}
