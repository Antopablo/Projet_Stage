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
    public class Mesures : INotifyPropertyChanged
    {
        [Key]
        public int Id { get; set; }

        private string _Nom;

        public string Nom
        {
            get { return _Nom; }
            set
            {
                _Nom = value;

                OnPropertyChanged("Nom");
            }
        }

        public ObservableCollection<Mesures> MesureObservableCollec { get; set; }

        private string _Description;

        public string Description
        {
            get { return _Description; }
            set
            {
                _Description = value;

                OnPropertyChanged("Description");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        private void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


        public Mesures(string nom, string description)
        {
            Nom = nom;
            Description = description;
            MesureObservableCollec = new ObservableCollection<Mesures>();
            Dico_Couleur = new Dictionary<STATUS, string>();
            RemplirDicoCouleur();
            Status = STATUS.non_evalue;
        }

        public Mesures(string nom)
        {
            Nom = nom;
            MesureObservableCollec = new ObservableCollection<Mesures>();
            Dico_Couleur = new Dictionary<STATUS, string>();
            RemplirDicoCouleur();
            Status = STATUS.non_evalue;
        }

        public Mesures(string nom, string description,int FKMesure,int FKProjet)
        {
            Nom = nom;
            Description = description;
            MesureObservableCollec = new ObservableCollection<Mesures>();
            Dico_Couleur = new Dictionary<STATUS, string>();
            RemplirDicoCouleur();
            Status = STATUS.non_evalue;
            FKToMesure = FKMesure;
            FKToProjets = FKProjet;
        }



        public int FKToProjets { get; set; }

        private string _Couleur;
        [NotMapped]
        public string Couleur
        {
            get { return _Couleur; }
            set
            {
                _Couleur = value;
                OnPropertyChanged("Couleur");
            }
        }

        [NotMapped]
        public Dictionary<STATUS, string> Dico_Couleur { get; set; }

        public void RemplirDicoCouleur()
        {
            Dico_Couleur.Add(STATUS.appliquee, "Green");
            Dico_Couleur.Add(STATUS.non_applicable, "DarkSlateGray");
            Dico_Couleur.Add(STATUS.non_appliquee, "Crimson");
            Dico_Couleur.Add(STATUS.non_evalue, "CadetBlue");
            Dico_Couleur.Add(STATUS.programmee, "Orange");
        }

        public void SetColor()
        {
            Couleur = Dico_Couleur[Status];
        }

        private STATUS _Status;

        public STATUS Status
        {
            get { return _Status; }
            set
            {
                _Status = value;
                SetColor();
                OnPropertyChanged("Status");
            }
        }

        public int FKToMesure { get; set; }

        public Mesures()
        {
            MesureObservableCollec = new ObservableCollection<Mesures>();
            Dico_Couleur = new Dictionary<STATUS, string>();
            RemplirDicoCouleur();
            Status = STATUS.non_evalue;
        }

    }
}
