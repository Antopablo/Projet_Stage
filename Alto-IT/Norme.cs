using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alto_IT
{
    class Norme
    {
        [Key]
        public int Id { get; set; }
        public string Nom_Norme { get; set; }
        public int IDNorme { get; set; }
    }
}
