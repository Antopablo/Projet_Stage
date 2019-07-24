using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alto_IT
{
    public enum PROVIDER
    {
        AWS,
        AZURE,
        GOOGLE
    }
    public class Projets : INotifyPropertyChanged
    {
        [key]
        public int Id { get; set; }

        private string _Nom;

        public string Nom
        {
            get { return _Nom; }
            set {_Nom = value;

                OnPropertyChanged("Nom");
                }
        }

        private void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private PROVIDER _CloudProvider;



        public PROVIDER CloudProvider
        {
            get { return _CloudProvider; }
            set { _CloudProvider = value;
                OnPropertyChanged("CloudProvider");
            }
        }

        public Projets()
        {

        }

        public Projets(string nom, PROVIDER cloudProvider)
        {
            Nom = nom;
            CloudProvider = cloudProvider;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
