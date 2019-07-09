using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alto_IT
{
    public class Norme
    {
        [Key]
        public int Id { get; set; }


        private string _Nom_Norme;

        public Norme(string nom_Norme)
        {
            Nom_Norme = nom_Norme;
            NormeObervCollec = new ObservableCollection<Norme>();
            
        }

        public Norme() { }

        public string Nom_Norme
        {
            get { return _Nom_Norme; }
            set
            {
                _Nom_Norme = value;
                OnPropertyChanged("Nom_Norme");
            }
        }
        public int IDNorme { get; set; }

        public ObservableCollection<Norme> NormeObervCollec { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public override string ToString()
        {
            return Nom_Norme;
        }
    }
}
