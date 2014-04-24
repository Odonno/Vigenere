using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Vigenere.Model;
using Vigenere.Model.Model;
using Vigenere.ViewModel.Services.Abstract;

namespace Vigenere.ViewModel.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private VigenereResolution _resolver = new VigenereResolution();
        public VigenereResolution Resolver
        {
            get { return _resolver; }
            set { _resolver = value; }
        }

        private MethodeResolution _methodeResolution;
        public MethodeResolution MethodeResolution
        {
            get
            {
                return _methodeResolution;
            }
            set
            {
                _methodeResolution = value;
                RaisePropertyChanged();
                Resolve();
            }
        }

        public IEnumerable<MethodeResolution> MethodesResolution { get; set; } 

        private readonly IResolverService _resolverService;


        public RelayCommand ResolveCommand { get; set; }
        public RelayCommand GenerateRandomVigenereTableCommand { get; set; }
        public RelayCommand RestoreDefaultVigenereTableCommand { get; set; }


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IResolverService resolverService)
        {
            _resolverService = resolverService;

            MethodeResolution = MethodeResolution.Chiffrement;
            MethodesResolution = Enum.GetValues(typeof(MethodeResolution)).Cast<MethodeResolution>();

            ResolveCommand = new RelayCommand(Resolve);
            GenerateRandomVigenereTableCommand = new RelayCommand(GenerateRandomVigenereTable);
            RestoreDefaultVigenereTableCommand = new RelayCommand(RestoreDefaultVigenereTable, CanRestoreDefaultVigenereTable);


            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
                _resolver.Texte = "JE SUIS UN CALAMAR ROUGE";
                _resolver.Cle = "TEXTE";
                Resolve();
            }
            else
            {
                // Code runs "for real"
            }
        }




        public async void Resolve()
        {
            // waiting because the Vigenere Table does not bind direclty to source
            await Task.Delay(1);

            if (!_resolver.Table.IsTableCorrect)
            {
                _resolver.Resultat = "La table n'est pas valide...";
                return;
            }

            _resolver.Table.ToUpper();

            switch (MethodeResolution)
            {
                case MethodeResolution.Chiffrement:
                    _resolverService.Chiffrer(_resolver);
                    break;

                case MethodeResolution.Dechiffrement:
                    _resolverService.Dechiffrer(_resolver);
                    break;

                case MethodeResolution.ChiffrementBeaufort:
                    _resolverService.ChiffrerBeaufort(_resolver);
                    break;

                case MethodeResolution.DechiffrementBeaufort:
                    _resolverService.DechiffrerBeaufort(_resolver);
                    break;
            }
        }

        public void GenerateRandomVigenereTable()
        {
            const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();
            _resolver.Table.Valeurs = new string[26, 26];

            for (short x = 0; x < 26; x++)
            {
                var temporaryAlphabet = alphabet;

                for (short y = 0; y < 26; y++)
                {
                    _resolver.Table.Valeurs[x, y] = temporaryAlphabet[random.Next(temporaryAlphabet.Length)].ToString();
                    temporaryAlphabet = temporaryAlphabet.Replace(_resolver.Table.Valeurs[x, y], "");
                }
            }
               

            Resolve();
        }

        public bool CanRestoreDefaultVigenereTable()
        {
            return !(_resolver.Table.Equals(new VigenereTable()));
        }
        public void RestoreDefaultVigenereTable()
        {
            _resolver.Table = new VigenereTable();

            Resolve();
        }
    }
}