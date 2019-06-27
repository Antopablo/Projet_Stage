using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alto_IT
{
    public class File
    {

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        [NotMapped]
        public string Path { get; set; }

        public File(string title, string path)
        {
            Title = title;
            Path = path;
        }

        public File()
        {
        }

    }
}
