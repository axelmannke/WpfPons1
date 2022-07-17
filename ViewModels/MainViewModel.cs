using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using CommonTypes;

namespace ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void notifyPropertyChanged(string propname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }
        public ObservableCollection<PonsTranslation> TranslationsObservableCollection { get; private set; } = new ObservableCollection<PonsTranslation>();

        private string _word;
        public string Word
        {
            get
            {
                return _word;
            }
            set
            {
                _word = value;
            }
        }

        public Commands.PonsResponseCommand GetPonsResponse { get; } = new Commands.PonsResponseCommand();


        private PonsApiModel pons = new PonsApiModel();

        public MainViewModel()
        {
            
        }

        internal void GetResponse(string word)
        {
            string json = pons.apiRequest(word, "deit");
           
            List<PonsTranslation> ptlst = pons.Parse(json);

            ptlst = pons.Parse(json);

            //TranslationsObservableCollection = new ObservableCollection<PonsTranslation>();

            TranslationsObservableCollection.Clear();

            foreach (PonsTranslation p in ptlst)
                TranslationsObservableCollection.Add(p);

            long id = DbModel.InsertWord(word, json);

            this.notifyPropertyChanged("GibLaut");
        }

    }
}
