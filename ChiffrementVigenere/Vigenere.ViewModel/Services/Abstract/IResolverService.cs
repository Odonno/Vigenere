using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vigenere.Model.Model;


namespace Vigenere.ViewModel.Services.Abstract
{
    public interface IResolverService
    {
        void Chiffrer(VigenereResolution resolver);
        void Dechiffrer(VigenereResolution resolver);

        void ChiffrerBeaufort(VigenereResolution resolver);
        void DechiffrerBeaufort(VigenereResolution resolver);
    }
}
