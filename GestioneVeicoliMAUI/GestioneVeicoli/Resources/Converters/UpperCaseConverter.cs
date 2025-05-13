using System.Globalization;

namespace GestioneVeicoli.Resources.Converters
{
    public class UpperCaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                return stringValue.ToUpper(); // Restituisce la stringa in maiuscolo
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                return stringValue.ToUpper(); // Garantisce che anche la conversione inversa mantenga i caratteri in maiuscolo
            }
            return value;
        }
    }
}
