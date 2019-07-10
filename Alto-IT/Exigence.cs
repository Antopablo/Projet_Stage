﻿using System;
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
    public class Exigence : INotifyPropertyChanged
    {
        public Exigence(string name, string description, int foreignkey, int foreignkeyTOnorme)
        {
            Name = name;
            Description = description;
            ForeignKey = foreignkey;
            ForeignKey_TO_Norme = foreignkeyTOnorme;
            this.ExigenceObervCollec = new ObservableCollection<Exigence>();
        }

        public Exigence(string name, int foreignkey, int foreignkeyTOnorme)
        {
            Name = name;
            ForeignKey = foreignkey;
            ForeignKey_TO_Norme = foreignkeyTOnorme;
            this.ExigenceObervCollec = new ObservableCollection<Exigence>();
            IDExigence = Id;
        }

        public Exigence () { this.ExigenceObervCollec = new ObservableCollection<Exigence>(); }

        [Key]
        public int Id { get; set; }

        [InverseProperty ("ForeignKey")]
        public int IDExigence { get; set; }

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

        public int ForeignKey_TO_Norme { get; set; }

        public ObservableCollection<Exigence> ExigenceObervCollec { get; set; }

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
