using System.Globalization;

namespace GestioneVeicoli.Resources.Converters
{
    public class StringToIntConverters : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (int.TryParse(value?.ToString(), out int result))
            {
                return result;
            }
            return 0; // Valore predefinito per input non valido o vuoto
        }
    }
}
