using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Alto_IT
{
    /// <summary>
    /// Logique d'interaction pour Ajout.xaml
    /// </summary>
    public partial class Ajout : Window
    {
        MainWindow mw;
        public int CompteurID { get; set; }
        public Ajout(MainWindow m)
        {
            InitializeComponent();
            mw = m;
        }

        private void ValiderNorme_Click(object sender, RoutedEventArgs e)
        {
            Norme N = new Norme(Title.Text, Content.Text);
            mw.database.NormesDatabase.Add(N);
            mw.database.SaveChanges();
            Close();
            CreateTable(Title.Text, 0 /* <= fonction LINQ qui retourne la FOREIGN KEY*/);
            RemplirTable(Title.Text, 0);
        }

        public void CreateTable(string TableName, int ForeignKey)
        {
            using (ApplicationDatabase context = new ApplicationDatabase())
            {
                var x = context.Database.ExecuteSqlCommand("CREATE TABLE "+TableName+"(ID INTEGER PRIMARY KEY, ForeignKey INT, Titre VARCHAR(1000), Description VARCHAR(1000))");
            }
        }

        public void RemplirTable(string TableName, int key)
        {
            using (ApplicationDatabase context = new ApplicationDatabase())
            {
                var x = context.Database.ExecuteSqlCommand("INSERT INTO " + TableName + "(ID, Titre, Description) VALUES (" + "'" + key + "'" + "," + "'" + Title.Text + "'" + "," + "'" + Content.Text + "'" + ")");
            }
        }
    }
}
