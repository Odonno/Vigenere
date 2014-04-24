using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vigenere.Model.Model;
using Vigenere.ViewModel.Helpers;
using Vigenere.ViewModel.Services.Abstract;

namespace Vigenere.ViewModel.Services.Concrete
{
    class ResolverService : IResolverService
    {
        public void Chiffrer(VigenereResolution resolver)
        {
            if (string.IsNullOrWhiteSpace(resolver.Cle) || string.IsNullOrWhiteSpace(resolver.Texte))
                resolver.Resultat = string.Empty;

            string r = string.Empty;
            int indexCle = 0;

            try
            {
                foreach (char car in resolver.Texte)
                {
                    if (!car.CanCryptChar(resolver.Cle, ref r, ref indexCle)) 
                        continue;

                    // Ligne = indice de la clé
                    int ligneIndex = resolver.Table.Ligne.IndexOf(resolver.Cle[indexCle].ToString());
                    // Colonne = indice du caractère clair
                    int colonneIndex = resolver.Table.Colonne.IndexOf(car.ToString());
                    
                    r += resolver.Table.Valeurs[ligneIndex, colonneIndex];

                    resolver.Cle.NextKeyIndex(ref indexCle);
                }
            }
            catch
            {
                resolver.Resultat = "Chiffrement impossible...";
                return;
            }

            resolver.Resultat = r;
        }
        public void Dechiffrer(VigenereResolution resolver)
        {
            if (string.IsNullOrWhiteSpace(resolver.Cle) || string.IsNullOrWhiteSpace(resolver.Texte))
                resolver.Resultat = string.Empty;

            string r = string.Empty;
            int indexCle = 0;

            try
            {
                foreach (char car in resolver.Texte)
                {
                    if (!car.CanCryptChar(resolver.Cle, ref r, ref indexCle))
                        continue;

                    // Ligne = indice de la clé
                    int keyIndex = resolver.Table.Ligne.IndexOf(resolver.Cle[indexCle].ToString());
                    // Colonne = indice du caractère chiffré dans la Ligne (L)
                    int cryptedColumnIndex = 0;

                    for (short x = 0; x < 26; x++)
                    {
                        if (resolver.Table.Valeurs[keyIndex, x] == car.ToString().ToUpper())
                        {
                            cryptedColumnIndex = x;
                            break;
                        }
                    }

                    r += resolver.Table.Colonne[cryptedColumnIndex];

                    resolver.Cle.NextKeyIndex(ref indexCle);
                }
            }
            catch
            {
                resolver.Resultat = "Déchiffrement impossible...";
                return;
            }

            resolver.Resultat = r;
        }

        public void ChiffrerBeaufort(VigenereResolution resolver)
        {
            if (string.IsNullOrWhiteSpace(resolver.Cle) || string.IsNullOrWhiteSpace(resolver.Texte))
                resolver.Resultat = string.Empty;

            string r = string.Empty;
            int indexCle = 0;

            try
            {
                foreach (char car in resolver.Texte)
                {
                    if (!car.CanCryptChar(resolver.Cle, ref r, ref indexCle))
                        continue;

                    // Ligne = indice du caractère clair
                    int ligneIndex = resolver.Table.Ligne.IndexOf(car.ToString());
                    // Colonne = indice de la clé dans la Ligne (L)
                    int colonneIndex = 0;

                    for (short x = 0; x < 26; x++)
                    {
                        if (resolver.Table.Valeurs[ligneIndex, x] == resolver.Cle[indexCle].ToString())
                        {
                            colonneIndex = x;
                            break;
                        }
                    }

                    r += resolver.Table.Colonne[colonneIndex];

                    resolver.Cle.NextKeyIndex(ref indexCle);
                }
            }
            catch
            {
                resolver.Resultat = "Chiffrement de Beaufort impossible...";
                return;
            }

            resolver.Resultat = r;
        }

        public void DechiffrerBeaufort(VigenereResolution resolver)
        {
            if (string.IsNullOrWhiteSpace(resolver.Cle) || string.IsNullOrWhiteSpace(resolver.Texte))
                resolver.Resultat = string.Empty;

            string r = string.Empty;
            int indexCle = 0;

            try
            {
                foreach (char car in resolver.Texte)
                {
                    if (!car.CanCryptChar(resolver.Cle, ref r, ref indexCle))
                        continue;

                    // Ligne : indice de la clé dans la Colonne (C)
                    int keyIndex = 0;
                    // Colonne : indice du caractère chiffré
                    int cryptedColumnIndex = resolver.Table.Colonne.IndexOf(car.ToString());

                    for (short x = 0; x < 26; x++)
                    {
                        if (resolver.Table.Valeurs[x, cryptedColumnIndex] == resolver.Cle[indexCle].ToString().ToUpper())
                        {
                            keyIndex = x;
                            break;
                        }
                    }

                    r += resolver.Table.Ligne[keyIndex];

                    resolver.Cle.NextKeyIndex(ref indexCle);
                }
            }
            catch
            {
                resolver.Resultat = "Déchiffrement de Beaufort impossible...";
                return;
            }

            resolver.Resultat = r;
        }
    }
}
