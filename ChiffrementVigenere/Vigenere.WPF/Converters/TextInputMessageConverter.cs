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
    class TextInputMessageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var methode = (MethodeResolution)value;

            switch (methode)
            {
                case MethodeResolution.Chiffrement:
                case MethodeResolution.ChiffrementBeaufort:
                    return "Texte en clair";

                case MethodeResolution.Dechiffrement:
                case MethodeResolution.DechiffrementBeaufort:
                    return "Texte chiffré";

                default:
                    return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
