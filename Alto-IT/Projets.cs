using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alto_IT
{
    enum PROVIDER
    {
        AWS,
        AZURE,
        GOOGLE
    }

    class Projets

    {
        public Projets()
        {

        }

        public Projets(string name, PROVIDER provider)
        {
            Name = name;
            Provider = provider;
        }


        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value;
                OnPropertyChanged("Name");
            }
        }


        private PROVIDER _provider;

        public PROVIDER Provider
        {
            get { return _provider; }
            set { _provider = value;
                OnPropertyChanged("Provider");
            }
        }

        public ObservableCollection<Projets> ProjetsObservCollec { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


    }
}
