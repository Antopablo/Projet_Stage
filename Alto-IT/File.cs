using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<File> ObservCollect { get; set; }
        public enum CATEGORIE
        {
            DELIVRABLE,
            PROCESS, 
            POLICIES
        }

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        [NotMapped]
        public string Path { get; set; }

        public CATEGORIE Category { get; set; }

        public File(string title, string path, CATEGORIE category)
        {
            Title = title;
            Path = path;
            Category = category;
        }

        public File() // indispensable pour les requêtes LINQ
        {
        }

        public override string ToString()
        {
            return Category.ToString();
        }

    }
}
