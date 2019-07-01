using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alto_IT
{
    public class Norme
    {
        public Norme(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public Norme(string name)
        {
            Name = name;
        }

        public Norme () { }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
