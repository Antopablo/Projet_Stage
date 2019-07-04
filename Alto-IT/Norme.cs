using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alto_IT
{
    public class Norme : INotifyPropertyChanged
    {
        public Norme(string name, string description, int foreignkey)
        {
            Name = name;
            Description = description;
            ForeignKey = foreignkey;
            this.NormeObervCollec = new ObservableCollection<Norme>();
        }

        public Norme(string name, int foreignkey)
        {
            Name = name;
            ForeignKey = foreignkey;
            this.NormeObervCollec = new ObservableCollection<Norme>();
        }

        public Norme () { this.NormeObervCollec = new ObservableCollection<Norme>(); }

        [Key]
        public int Id { get; set; }


        private string _Name;
        public string Name
        {
            get { return _Name; }
            set {
                _Name = value;
                OnPropertyChanged("Name");
            }
        }


        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value;
                    OnPropertyChanged("Description");
                }
        }


        public int ForeignKey { get; set; }

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
            return Name;
        }


    }
}
