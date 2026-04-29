using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CarManager.Core.Extensions
{
    public static class BirthPlaceNormalizer
    {
        public static string NormalizeBirthPlace(string birthPlace)
        {
            if (string.IsNullOrWhiteSpace(birthPlace))
                return string.Empty;
            
            // Remove leading and trailing whitespace
            birthPlace = birthPlace.Trim()
                         .Replace("’", "'")
                         .Replace("`", "'")
                         .Replace("'", " ");

            birthPlace = Regex.Replace(birthPlace, @"[^A-Za-z\s]", " ");
            birthPlace = Regex.Replace(birthPlace, @"\s+", " ").Trim();
            return birthPlace;
        }
    }
}
