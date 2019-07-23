﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alto_IT
{
    public class RelationMesureExigence
    {
        public RelationMesureExigence(int idMesure, int idExigence)
        {
            IdMesure = idMesure;
            IdExigence = idExigence;
        }

        [Key]
        public int Id { get; set; }
        public int IdMesure { get; set; }
        public int IdExigence { get; set; }


    }
}
