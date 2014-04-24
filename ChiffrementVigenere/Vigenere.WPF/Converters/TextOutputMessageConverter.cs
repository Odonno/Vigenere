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
    class TextOutputMessageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var methode = (MethodeResolution)value;

            switch (methode)
            {
                case MethodeResolution.Chiffrement:
                case MethodeResolution.ChiffrementBeaufort:
                    return "Message chiffré";

                case MethodeResolution.Dechiffrement:
                case MethodeResolution.DechiffrementBeaufort:
                    return "Message en clair";

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
