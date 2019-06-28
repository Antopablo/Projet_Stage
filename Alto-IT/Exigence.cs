using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alto_IT
{
    enum ETAT
    {
        NON_APPLICABLE,
        NON_APPLIQUE,
        PROGRAMME,
        APPLIQUE
    }

    class Exigence
    {
        public string Name { get; set; }
        public bool Statut { get; set; }
        public string Description { get; set; }

        private ETAT _Etat;

        public ETAT Etat
        {
            get { return _Etat; }
            set { _Etat = value; }
        }

        public string PathJustificatif { get; set; }

        public Exigence(string name, bool statut, string description, ETAT etat)
        {
            Name = name;
            Statut = statut;
            Description = description;
            Etat = etat;
        }

        



    }
}
