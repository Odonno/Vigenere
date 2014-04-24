using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Vigenere.Model.Model;

namespace Vigenere.WPF.Converters
{
    class MethodeResolutionToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var methode = (MethodeResolution)value;

            switch (methode)
            {
                case MethodeResolution.Chiffrement:
                    return "Méthode de chiffrement de Vigenère";

                case MethodeResolution.Dechiffrement:
                    return "Méthode de déchiffrement de Vigenère";

                case MethodeResolution.ChiffrementBeaufort:
                    return "Méthode de chiffrement de Beaufort";

                case MethodeResolution.DechiffrementBeaufort:
                    return "Méthode de déchiffrement de Beaufort";   
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var methode = value as string;

            if (methode != null)
                switch (methode)
                {
                    case "Méthode de chiffrement de Vigenère":
                        return MethodeResolution.Chiffrement;

                    case "Méthode de déchiffrement de Vigenère":
                        return MethodeResolution.Dechiffrement;

                    case "Méthode de chiffrement de Beaufort":
                        return MethodeResolution.ChiffrementBeaufort;

                    case "Méthode de déchiffrement de Beaufort":
                        return MethodeResolution.DechiffrementBeaufort;
                }

            return MethodeResolution.Chiffrement;
        }
    }
}
