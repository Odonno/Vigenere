using System;
using PropertyChanged;

namespace Vigenere.Model.Model
{
    [ImplementPropertyChanged]
    public class VigenereResolution
    {
        private string _texte;
        public string Texte { get { return _texte; } set { _texte = value.ToUpper(); } }

        private string _cle;
        public string Cle { get { return _cle; } set { _cle = value.ToUpper(); } }

        private VigenereTable _table = new VigenereTable();
        public VigenereTable Table { get { return _table; } set { _table = value; } }

        public string Resultat { get; set; }
    }
}
