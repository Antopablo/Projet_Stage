using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alto_IT
{
    public class Norme
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

        public string Name { get; set; }

        public string Description { get; set; }

        public int ForeignKey { get; set; }

        public ObservableCollection<Norme> NormeObervCollec { get; set; }

        public override string ToString()
        {
            return Name;
        }


    }
}
