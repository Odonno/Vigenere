using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vigenere.ViewModel.Helpers
{
    static class ResolverHelper
    {
        public static void NextKeyIndex(this string cle, ref int indexCle)
        {
            if (++indexCle >= cle.Length)
                indexCle = 0;
        }

        public static bool CanCryptChar(this Char car, string cle, ref string resultat, ref int indexCle)
        {
            if (string.IsNullOrWhiteSpace(car.ToString()))
            {
                resultat += " ";
                return false;
            }
            while (string.IsNullOrWhiteSpace(cle[indexCle].ToString()))
                cle.NextKeyIndex(ref indexCle);
            return true;
        }
    }
}
