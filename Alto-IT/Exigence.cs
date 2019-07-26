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
    public enum STATUS
    {
        non_evalue,
        non_appliquee,
        programmee,
        appliquee,
        non_applicable
    }

    public class Exigence : INotifyPropertyChanged
    {
        public Exigence(string name, string description, int foreignkey, int foreignkeyTOnorme,int FKProjet)
        {
            Name = name;
            Description = description;
            ForeignKey = foreignkey;
            ForeignKey_TO_Norme = foreignkeyTOnorme;
            FKToProjet = FKProjet;
            IdExigence = Id;
            this.ExigenceObervCollec = new ObservableCollection<Exigence>();
            Dico_Couleur = new Dictionary<STATUS, string>();
            RemplirDicoCouleur();
            Status = STATUS.non_evalue;
            ListeNomMesuresAssociees = new ObservableCollection<string>();
            Dico_MesuresCheck = new Dictionary<Mesures, bool>();
        }

        public Exigence(string name, int foreignkey, int foreignkeyTOnorme, int FKProjet)
        {
            Name = name;
            ForeignKey = foreignkey;
            ForeignKey_TO_Norme = foreignkeyTOnorme;
            FKToProjet = FKProjet;
            IdExigence = Id;
            this.ExigenceObervCollec = new ObservableCollection<Exigence>();
            Dico_Couleur = new Dictionary<STATUS, string>();
            RemplirDicoCouleur();
            Status = STATUS.non_evalue;
            ListeNomMesuresAssociees = new ObservableCollection<string>();
            Dico_MesuresCheck = new Dictionary<Mesures, bool>();
        }

        public Exigence()
        {
            this.ExigenceObervCollec = new ObservableCollection<Exigence>();
            Dico_Couleur = new Dictionary<STATUS, string>();
            RemplirDicoCouleur();
            Status = STATUS.non_evalue;
            Dico_MesuresCheck = new Dictionary<Mesures, bool>();
        }

        private Dictionary<Mesures, bool> _Dico_MesuresCheck;

        public Dictionary<Mesures, bool> Dico_MesuresCheck
        {
            get { return _Dico_MesuresCheck; }
            set
            {
                _Dico_MesuresCheck = value;
                OnPropertyChanged("Dico_ExigenceCheck");
            }
        }

        public ObservableCollection<string> ListeNomMesuresAssociees { get; set; }


        [Key]
        public int Id { get; set; }

        [InverseProperty ("ForeignKey")]
        public int IdExigence { get; set; }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set {
                _Name = value;
                OnPropertyChanged("Name");
            }
        }

        private bool _IsChecked;

        public bool Ischecked
        {
            get { return _IsChecked; }
            set { _IsChecked = value;
                OnPropertyChanged("Ischecked");
            }
        }


        private STATUS _Status;

        public STATUS Status
        {
            get { return _Status; }
            set {
                _Status = value;
                SetColor();
                OnPropertyChanged("Status");
            }
        }

        [NotMapped]
        public Dictionary<STATUS,string> Dico_Couleur { get; set; }


        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value;
                    OnPropertyChanged("Description");
                }
        }

        public int ForeignKey { get; set; }

        public int FKToProjet { get; set; }

        public int ForeignKey_TO_Norme { get; set; }

        private string _Couleur;

        [NotMapped]
        public string Couleur
        {
            get { return _Couleur; }
            set {
                _Couleur = value;
                OnPropertyChanged("Couleur");
            }
        }

        private string _DocumentPath;

        public string DocumentPath
        {
            get { return _DocumentPath; }
            set {
                _DocumentPath = value;
                OnPropertyChanged("DocumentPath");
            }
        }

        private string _DocumentName;

        public string DocumentName
        {
            get { return _DocumentName; }
            set {
                _DocumentName = value;
                OnPropertyChanged("DocumentName"); 
            }
        }


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
    }
}
