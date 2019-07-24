using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alto_IT
{
    public class RelationsMesuresExigences
    {

        [key]
        public int Id { get; set; }

        public int IdExigence { get; set; }

        public int IdMesures { get; set; }


        public RelationsMesuresExigences(int idExigence, int idMesures)
        {
            IdExigence = idExigence;
            IdMesures = idMesures;
        }


        public RelationsMesuresExigences()
        {

        }

    }
}
